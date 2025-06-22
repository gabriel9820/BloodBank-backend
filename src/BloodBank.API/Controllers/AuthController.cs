using BloodBank.Application.Commands.Login;
using BloodBank.Application.Commands.Register;
using BloodBank.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        SetTokensInCookies(result.Data!);

        return NoContent();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return Created();
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("X-Access-Token");
        Response.Cookies.Delete("X-Refresh-Token");

        return NoContent();
    }

    private void SetTokensInCookies(LoginViewModel tokens)
    {
        var options = new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Secure = true,
            Expires = DateTime.UtcNow.AddMonths(3)
        };

        Response.Cookies.Append("X-Access-Token", tokens.AccessToken, options);
        Response.Cookies.Append("X-Refresh-Token", tokens.RefreshToken, options);
    }
}

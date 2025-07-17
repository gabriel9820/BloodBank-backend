using BloodBank.Application.Commands.AddDonation;
using BloodBank.Application.Commands.DeleteDonation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DonationsController(
    IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Add(AddDonationCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return Created();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteDonationCommand(id));

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return NoContent();
    }
}

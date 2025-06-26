using BloodBank.Application.Commands.AddDonor;
using BloodBank.Application.Commands.DeleteDonor;
using BloodBank.Application.Queries.GetAllDonors;
using BloodBank.Application.Queries.GetDonorById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DonorsController(
    IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllDonorsQuery query)
    {
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetDonorByIdQuery(id));

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddDonorCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteDonorCommand(id));

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return NoContent();
    }
}

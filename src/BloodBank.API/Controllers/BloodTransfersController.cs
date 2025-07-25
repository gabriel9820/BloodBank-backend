using BloodBank.Application.Commands.AddBloodTransfer;
using BloodBank.Application.Commands.DeleteBloodTransfer;
using BloodBank.Application.Queries.GetAllBloodTransfers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BloodTransfersController(
    IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBloodTransfersQuery query)
    {
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddBloodTransferCommand command)
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
        var result = await _mediator.Send(new DeleteBloodTransferCommand(id));

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return NoContent();
    }
}

using BloodBank.Application.Commands.AddHospital;
using BloodBank.Application.Commands.DeleteHospital;
using BloodBank.Application.Commands.UpdateHospital;
using BloodBank.Application.Queries.GetAllHospitals;
using BloodBank.Application.Queries.GetHospitalById;
using BloodBank.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HospitalsController(
    IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllHospitalsQuery query)
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
        var result = await _mediator.Send(new GetHospitalByIdQuery(id));

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<IActionResult> Add(AddHospitalCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<IActionResult> Update(int id, UpdateHospitalCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteHospitalCommand(id));

        if (!result.IsSuccess)
        {
            return StatusCode(result.Error.Code, result.Error.Message);
        }

        return NoContent();
    }
}

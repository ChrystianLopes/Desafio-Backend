using Microsoft.AspNetCore.Mvc;
using RentalPlatform.Application.Motorcycles.Commands.MotorcycleRegistration;
using RentalPlatform.Application.Motorcycles.Commands.UpdateMotorcycleLicensePlate;
using RentalPlatform.Application.Motorcycles.Queries.GetMotorcycles;
using RentalPlatform.Application.Common.Exceptions;
using RentalPlatform.Application.Motorcycles.Queries.GetMotorcycleByIdentifier;
using RentalPlatform.Application.Motorcycles.Commands.DeleteMotorcycle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalPlatform.Api.Controllers;

[Route("/motos")]
public class MotosController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(MotorcycleRegistrationCommand command)
    {
        var identifier = await Mediator.Send(command);
        return Created($"/api/motos/{identifier}", null);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MotorcycleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] string? placa)
    {
        var query = new GetMotorcyclesQuery(placa);
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateLicensePlate(string id, [FromBody] UpdateMotorcycleLicensePlateCommand command)
    {
        command.Identifier = id;
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MotorcycleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetMotorcycleByIdentifierQuery(id);
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteMotorcycleCommand(id);
        await Mediator.Send(command);
        return Ok();
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalPlatform.Application.Rentals.Commands.CreateRental;
using RentalPlatform.Application.Rentals.Commands.ReturnMotorcycle;
using System;
using System.Threading.Tasks;

namespace RentalPlatform.Api.Controllers;

[Route("api/locacao")]
public class LocacaoController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateRentalCommand command)
    {
        try
        {
            await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (InvalidOperationException)
        {
            return BadRequest(new { mensagem = "Dados inválidos" });
        }
    }

    [HttpPost("devolucao")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReturnMotorcycle(ReturnMotorcycleCommand command)
    {
        try
        {
            var finalCost = await Mediator.Send(command);
            return Ok(new { valor_total = finalCost });
        }
        catch (InvalidOperationException ex)
        {
            // Retorna a mensagem específica da exceção para dar mais detalhes do erro.
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}
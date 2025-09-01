using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalPlatform.Application.Drivers.Commands.CreateDriver;
using RentalPlatform.Application.Drivers.Commands.UploadCnhImage;
using System;
using System.Threading.Tasks;

namespace RentalPlatform.Api.Controllers;

[Route("/entregadores")]
public class EntregadoresController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateDriverCommand command)
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

    [HttpPost("{id:guid}/cnh")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadCnhImage(Guid id, IFormFile cnh)
    {
        if (cnh == null || cnh.Length == 0)
        {
            return BadRequest(new { mensagem = "Dados inválidos" });
        }

        try
        {
            await using var stream = cnh.OpenReadStream();
            var command = new UploadCnhImageCommand(id, stream, cnh.FileName, cnh.ContentType);
            await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (InvalidOperationException)
        {
            return BadRequest(new { mensagem = "Dados inválidos" });
        }
    }
}
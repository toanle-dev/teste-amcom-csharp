namespace Questao5.Infrastructure.Services.Controllers;

using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Commands.Responses;

[ApiController]
[Route("api/saldo")]
public class SaldoController : ControllerBase
{
    private readonly IMediator _mediator;

    public SaldoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{idContaCorrente}")]
    public async Task<IActionResult> ObterSaldo(string idContaCorrente)
    {
        try
        {
            var response = await _mediator.Send(new SaldoContaCorrenteRequest
            {
                IdContaCorrente = idContaCorrente
            });
            return Ok(response);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ErrorResponse
            {
                Mensagem = ex.Message,
                TipoFalha = ex.TipoFalha
            });
        }
    }
}
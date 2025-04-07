namespace Questao5.Infrastructure.Services.Controllers;

using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Request;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;


[ApiController]
[Route("api/movimentacao")]
public class MovimentacaoController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovimentacaoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> MovimentarConta([FromBody] MovimentacaoContaCorrenteRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
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
namespace Questao5.Application.Commands.Request;

using MediatR;
using Questao5.Application.Commands.Responses;

public class MovimentacaoContaCorrenteRequest : IRequest<MovimentacaoContaCorrenteResponse>
{
    public string IdRequisicao { get; set; } = string.Empty;
    public string IdContaCorrente { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public char TipoMovimento { get; set; }
}
namespace Questao5.Application.Queries.Requests;

using MediatR;
using Questao5.Application.Queries.Responses;

public class SaldoContaCorrenteRequest : IRequest<SaldoContaCorrenteResponse>
{
    public string IdContaCorrente { get; set; } = string.Empty;
}
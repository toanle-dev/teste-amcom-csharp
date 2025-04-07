namespace Questao5.Application.Queries.Handlers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

public class SaldoContaCorrenteHandler : IRequestHandler<SaldoContaCorrenteRequest, SaldoContaCorrenteResponse>
{
    private readonly IDatabaseBootstrap _databaseBootstrap;

    public SaldoContaCorrenteHandler(IDatabaseBootstrap databaseBootstrap)
    {
        _databaseBootstrap = databaseBootstrap;
    }

    public async Task<SaldoContaCorrenteResponse> Handle(SaldoContaCorrenteRequest request, CancellationToken cancellationToken)
    {
        // Obter conta
        var conta = await ObterContaCorrente(request.IdContaCorrente);

        if (conta == null)
            throw new BusinessException("Conta corrente não encontrada", "INVALID_ACCOUNT");

        if (conta.Ativo == 0)
            throw new BusinessException("Conta corrente inativa", "INACTIVE_ACCOUNT");

        // Calcular saldo
        var saldo = await CalcularSaldo(request.IdContaCorrente);

        return new SaldoContaCorrenteResponse
        {
            Numero = conta.Numero,
            NomeTitular = conta.Nome,
            DataHoraConsulta = DateTime.Now,
            Saldo = saldo
        };
    }

    private async Task<ContaCorrente> ObterContaCorrente(string idContaCorrente)
    {
        using var connection = _databaseBootstrap.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(
            "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente",
            new { IdContaCorrente = idContaCorrente });
    }

    private async Task<decimal> CalcularSaldo(string idContaCorrente)
    {
        using var connection = _databaseBootstrap.GetConnection();

        var creditos = await connection.QueryFirstOrDefaultAsync<decimal>(
            "SELECT COALESCE(SUM(valor), 0) FROM movimento " +
            "WHERE idcontacorrente = @IdContaCorrente AND tipomovimento = 'C'",
            new { IdContaCorrente = idContaCorrente });

        var debitos = await connection.QueryFirstOrDefaultAsync<decimal>(
            "SELECT COALESCE(SUM(valor), 0) FROM movimento " +
            "WHERE idcontacorrente = @IdContaCorrente AND tipomovimento = 'D'",
            new { IdContaCorrente = idContaCorrente });

        return creditos - debitos;
    }
}
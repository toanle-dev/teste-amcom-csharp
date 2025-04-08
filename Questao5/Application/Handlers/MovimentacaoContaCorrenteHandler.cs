namespace Questao5.Application.Handlers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using Questao5.Infrastructure.Sqlite;
using Questao5.Application.Commands.Request;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;



public class MovimentacaoContaCorrenteHandler : IRequestHandler<MovimentacaoContaCorrenteRequest, MovimentacaoContaCorrenteResponse>
{
    private readonly IDatabaseBootstrap _databaseBootstrap;

    public MovimentacaoContaCorrenteHandler(IDatabaseBootstrap databaseBootstrap)
    {
        _databaseBootstrap = databaseBootstrap;
    }

    public async Task<MovimentacaoContaCorrenteResponse> Handle(MovimentacaoContaCorrenteRequest request, CancellationToken cancellationToken)
    {
        // Validações
        var conta = await ObterContaCorrente(request.IdContaCorrente);

        if (conta == null)
            throw new BusinessException("Conta corrente não encontrada", "INVALID_ACCOUNT");

        if (conta.Ativo == 0)
            throw new BusinessException("Conta corrente inativa", "INACTIVE_ACCOUNT");

        if (request.Valor <= 0)
            throw new BusinessException("Valor deve ser positivo", "INVALID_VALUE");

        if (request.TipoMovimento != 'C' && request.TipoMovimento != 'D')
            throw new BusinessException("Tipo de movimento inválido", "INVALID_TYPE");

        // Verificar idempotência
        var idempotencia = await VerificarIdempotencia(request.IdRequisicao);
        if (idempotencia != null)
        {
            return JsonConvert.DeserializeObject<MovimentacaoContaCorrenteResponse>(idempotencia.Resultado)!;
        }

        // Persistir movimento
        var idMovimento = Guid.NewGuid().ToString();
        var dataMovimento = DateTime.Now.ToString("dd/MM/yyyy");

        using var connection = _databaseBootstrap.GetConnection();
        await connection.ExecuteAsync(
            "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
            "VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)",
            new
            {
                IdMovimento = idMovimento,
                IdContaCorrente = request.IdContaCorrente,
                DataMovimento = dataMovimento,
                TipoMovimento = request.TipoMovimento.ToString(),
                Valor = request.Valor
            });

        // Registrar idempotência
        var response = new MovimentacaoContaCorrenteResponse { IdMovimento = idMovimento };
        await RegistrarIdempotencia(request.IdRequisicao, request, response);

        return response;
    }

    private async Task<ContaCorrente> ObterContaCorrente(string idContaCorrente)
    {
        using var connection = _databaseBootstrap.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(
            "SELECT * FROM contacorrente WHERE idcontacorrente = @IdContaCorrente",
            new { IdContaCorrente = idContaCorrente });
    }

    private async Task<Idempotencia> VerificarIdempotencia(string chaveIdempotencia)
    {
        using var connection = _databaseBootstrap.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<Idempotencia>(
            "SELECT * FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia",
            new { ChaveIdempotencia = chaveIdempotencia });
    }

    private async Task RegistrarIdempotencia(string chaveIdempotencia, object requisicao, object resultado)
    {
        using var connection = _databaseBootstrap.GetConnection();
        await connection.ExecuteAsync(
            "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) " +
            "VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)",
            new
            {
                ChaveIdempotencia = chaveIdempotencia,
                Requisicao = JsonConvert.SerializeObject(requisicao),
                Resultado = JsonConvert.SerializeObject(resultado)
            });
    }
}


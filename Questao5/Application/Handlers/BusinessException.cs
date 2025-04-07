namespace Questao5.Application.Handlers;


public class BusinessException : Exception
{
    public string TipoFalha { get; }

    public BusinessException(string mensagem, string tipoFalha) : base(mensagem)
    {
        TipoFalha = tipoFalha;
    }
}
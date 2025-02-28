namespace Questao5.Domain.Interfaces
{
    public interface ISaldoRepository
    {
        Task<double> ObterSaldo(string id);
    }
}

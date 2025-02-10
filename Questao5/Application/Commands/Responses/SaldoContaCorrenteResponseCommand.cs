using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Responses
{
    public class SaldoContaCorrenteResponseCommand
    {
        public int ContaCorrente { get; set; }
        public string Titular { get; set; } = string.Empty;
        public string DataHoraConsulta { get; set; } = string.Empty;
        public double Saldo { get; set; }

        public void MontarObjetoSaldoContaCorrente(ContaCorrete contaCorrente, double saldo) 
        {
            if (!string.IsNullOrEmpty(contaCorrente.Numero.ToString())) ContaCorrente = contaCorrente.Numero;
            if (!string.IsNullOrEmpty(contaCorrente.Nome)) Titular = contaCorrente.Nome;
            DataHoraConsulta = DateTime.Now.ToString("yyy-MM-dd HH:mm");
            Saldo = Math.Round(saldo, 2);
        }
    }
}

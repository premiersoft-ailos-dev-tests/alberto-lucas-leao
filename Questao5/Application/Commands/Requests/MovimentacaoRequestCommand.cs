using MediatR;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentacaoRequestCommand : IRequest<string>
    {
        public MovimentacaoRequestCommand() { }

        public MovimentacaoRequestCommand(string idRequisicao, int numeroConta, double valor, TipoMovimentacao tipo)
        {
            IdRequisicao = idRequisicao;
            NumeroConta = numeroConta;
            Valor = valor;
            Tipo = tipo;
        }

        public string IdRequisicao { get; private set; }
        public int NumeroConta { get; private set; }
        public double Valor { get; private set; }
        public TipoMovimentacao Tipo { get; private set; }
    }
}

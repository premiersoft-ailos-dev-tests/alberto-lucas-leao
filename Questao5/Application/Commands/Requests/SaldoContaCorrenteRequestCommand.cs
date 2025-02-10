using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class SaldoContaCorrenteRequestCommand : IRequest<SaldoContaCorrenteResponseCommand>
    {
        public SaldoContaCorrenteRequestCommand() { } 

        public SaldoContaCorrenteRequestCommand(int numeroContaCorrete)
        {
            NumeroContaCorrete = numeroContaCorrete;
        }

        public int NumeroContaCorrete { get; set; }
    }
}

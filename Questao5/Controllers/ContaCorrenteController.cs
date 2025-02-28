using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Input;

namespace Questao5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(IMediator));
        }

        /// <summary>
        /// Registra nova movimentação da conta corrente
        /// </summary>
        /// <param name="input">Contém os dados da movimentação</param>
        /// <returns>Retorna a identificação da movimentãção</returns>
        [HttpPost("movimentacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Movimentacao(MovimentacaoInput input) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            try
            {
                var request = new MovimentacaoRequestCommand(input.IdRequisicao,
                                                             input.NumeroConta,
                                                             input.Valor,
                                                             input.Tipo);

                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }
            
        }

        /// <summary>
        /// Obtém o daldo da conta corrente
        /// </summary>
        /// <param name="numeroContaCorrete">Contém o identificador da conta</param>
        /// <returns>Retorna o saldo e os dados da conta</returns>
        [HttpGet("saldo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ConsultarSaldo(int numeroContaCorrete)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var request = new SaldoContaCorrenteRequestCommand(numeroContaCorrete);
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }           
        }
    }
}

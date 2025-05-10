using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using SistemaCompra.Domain.Core;

namespace SistemaCompra.API.SolicitacaoCompra
{
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SolicitacaoCompraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("solicitacao/cadastro")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Cadastrar([FromBody]RegistrarCompraCommand registrarCompraCommand)
        {
            try
            {
                _mediator.Send(registrarCompraCommand);
                
                return StatusCode(201);
            }
            catch (BusinessRuleException)
            {
                return StatusCode(401);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
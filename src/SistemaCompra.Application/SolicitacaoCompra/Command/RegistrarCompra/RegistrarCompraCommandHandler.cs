using System;
using MediatR;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using System.Threading.Tasks;
using System.Threading;
using SistemaCompra.Infra.Data.UoW;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ProdutoAgg.IProdutoRepository _produtoRepository;
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;

        public RegistrarCompraCommandHandler(IUnitOfWork uow, IMediator mediator,
                                             SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository,
                                             ProdutoAgg.IProdutoRepository produtoRepository) : base(uow, mediator)
        {
            _produtoRepository = produtoRepository;
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);

                var itens = new Itens(_produtoRepository);
                solicitacaoCompra.RegistrarCompra(itens.ConvercaoItens(request.Itens));

                _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

                Commit();
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true); 
        }
    }
}
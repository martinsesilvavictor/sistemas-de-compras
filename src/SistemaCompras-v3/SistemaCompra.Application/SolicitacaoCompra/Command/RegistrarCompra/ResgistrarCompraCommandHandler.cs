using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Infra.Data.UoW;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ResgistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoCompraAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly ProdutoAgg.IProdutoRepository _produtoRepository;

        public ResgistrarCompraCommandHandler(SolicitacaoCompraAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, ProdutoAgg.IProdutoRepository produtoRepository,
                                                IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var solicitacaoCompra = new SolicitacaoCompraAgg.SolicitacaoCompra(request.NomeFornecedor, request.UsuarioSolicitante);

                var convert = Convert(request.Itens);
                solicitacaoCompra.RegistrarCompra(convert);

                _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

                Commit();
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public List<SolicitacaoCompraAgg.Item> Convert(List<Item> itens)
        {
            var aux = new List<SolicitacaoCompraAgg.Item>();

            foreach (var i in itens)
            {
                var produto = _produtoRepository.Obter(i.Id);
                aux.Add(new SolicitacaoCompraAgg.Item(produto, i.Qtde));
            }
            return aux;
        }
    }
}

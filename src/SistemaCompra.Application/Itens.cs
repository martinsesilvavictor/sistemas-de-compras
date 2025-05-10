using System.Collections.Generic;
using SistemaCompra.Application.SolicitacaoCompra.Command.Query.RegistrarCompra;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application
{
    public class Itens
    {
        private readonly ProdutoAgg.IProdutoRepository _produtoRepository;

        public Itens(ProdutoAgg.IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public List<SolicitacaoCompraAgg.Item> ConvercaoItens(List<ItemDTO> itens)
        {
            var aux = new List<SolicitacaoCompraAgg.Item>();
            
            foreach (var item in itens) 
            {
                var produto = _produtoRepository.Obter(item.Id);
                aux.Add(new SolicitacaoCompraAgg.Item(produto, item.Quantidade));
            }

            return aux;
        } 
    }
}

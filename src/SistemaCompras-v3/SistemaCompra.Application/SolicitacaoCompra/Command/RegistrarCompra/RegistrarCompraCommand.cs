using MediatR;
using System;
using System.Collections.Generic;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public string UsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public List<Item> Itens { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Qtde { get; set; }
    }
}

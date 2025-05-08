using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompraAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompraAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");
            builder.OwnsOne(u => u.UsuarioSolicitante, n => n.Property("Nome").HasColumnName("UsuarioSolicitante"));
            builder.OwnsOne(f => f.NomeFornecedor, n => n.Property("Nome").HasColumnName("NomeFornecedor"));
            builder.OwnsOne(t => t.TotalGeral, v => v.Property("Value").HasColumnName("TotalGeral"));
            builder.OwnsOne(c => c.CondicaoPagamento, v => v.Property("Valor").HasColumnName("CondicaoPagamento"));
        }
    }
}

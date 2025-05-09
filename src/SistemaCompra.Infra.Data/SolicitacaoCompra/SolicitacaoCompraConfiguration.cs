using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoCompraAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoCompraAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");
            builder.OwnsOne(u => u.UsuarioSolicitante, n => n.Property("Nome").HasColumnName("UsuarioSolicitante"));
            builder.OwnsOne(n => n.NomeFornecedor, n => n.Property("Nome").HasColumnName("NomeFornecedor"));
            builder.OwnsOne(t => t.TotalGeral, n => n.Property("Value").HasColumnName("TotalGeral"));
            builder.OwnsOne(n => n.CondicaoPagamento, n => n.Property("Valor").HasColumnName("CondicaoPagamento"));
        }
    }
}
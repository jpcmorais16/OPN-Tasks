using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPN.Domain;

namespace OPN.Data.Mappings;

public class ProportionMapping: IEntityTypeConfiguration<InstitutionProportion>
{
    public void Configure(EntityTypeBuilder<InstitutionProportion> builder)
    {
        builder.ToTable("Proporcao");
        builder.HasKey(p => new { p.ProductId, p.InstitutionId });

        builder.Property(p => p.ProductId).HasColumnName("Produto_id");
        builder.Property(p => p.InstitutionId).HasColumnName("Instituicao_id");
        builder.Property(p => p.Value).HasColumnName("Valor");
        builder.Property(p => p.Status).HasColumnName("Status");
    }
}
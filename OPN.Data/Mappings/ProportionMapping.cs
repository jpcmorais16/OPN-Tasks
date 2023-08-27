using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPN.Domain;

namespace OPN.Data.Mappings;

public class ProportionMapping: IEntityTypeConfiguration<InstitutionProportion>
{
    public void Configure(EntityTypeBuilder<InstitutionProportion> builder)
    {
        builder.ToTable("proporcao");
        builder.HasKey(p => new { p.ProductId, p.InstitutionId });

        builder.Property(p => p.ProductId).HasColumnName("Produto_id");
        builder.Property(p => p.InstitutionId).HasColumnName("Instituicao_id");
        builder.Property(p => p.Value).HasColumnName("Valor");
        builder.Property(p => p.Status).HasColumnName("Status");

        builder.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId);
        
        builder.HasOne(p => p.Institution)
            .WithMany()
            .HasForeignKey(p => p.InstitutionId);
    }
}
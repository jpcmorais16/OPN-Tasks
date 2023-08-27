using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPN.Domain;

namespace OPN.Data.Mappings;

public class ProductMapping: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("produto");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Codigo");
        builder.Property(p => p.Name).HasColumnName("Nome");
        builder.Property(p => p.InitialAmount).HasColumnName("Qtd_inicial");
        builder.Property(p => p.CurrentAmount).HasColumnName("Qtd_atual");
    }
}
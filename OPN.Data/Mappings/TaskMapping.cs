using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPN.Domain.Tasks;

namespace OPN.Data.Mappings;

public class TaskMapping: IEntityTypeConfiguration<OPNProductHandlingTask>
{
    public void Configure(EntityTypeBuilder<OPNProductHandlingTask> builder)
    {
        builder.ToTable("Task");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Codigo");
        builder.Property(p => p.ProductId).HasColumnName("Produto_id");
        builder.Property(p => p.InstitutionId).HasColumnName("Instituicao_id");
        builder.Property(p => p.Amount).HasColumnName("Quantidade");

        builder.HasOne(p => p.Institution)
            .WithMany()
            .HasForeignKey(p => p.InstitutionId);
        
        builder.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId);
    }
}
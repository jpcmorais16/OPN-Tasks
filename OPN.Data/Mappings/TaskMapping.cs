using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPN.Domain.Tasks;

namespace OPN.Data.Mappings;

public class TaskMapping: IEntityTypeConfiguration<OPNProductHandlingTask>
{
    public void Configure(EntityTypeBuilder<OPNProductHandlingTask> builder)
    {
        builder.ToTable("task");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Codigo");
        builder.Property(p => p.ProductId).HasColumnName("Produto_id");
        builder.Property(p => p.InstitutionId).HasColumnName("Instituicao_id");
        builder.Property(p => p.UserIdn).HasColumnName("Usuario_idn");
        builder.Property(p => p.Amount).HasColumnName("Quantidade");
        builder.Property(p => p.Status).HasColumnName("Status");
        builder.Property(p => p.CreationTime).HasColumnName("MomentoCriacao");
        builder.Property(p => p.CancelTime).HasColumnName("MomentoCancelamento");
        builder.Property(p => p.ConclusionTime).HasColumnName("MomentoConclusao");

        builder.HasOne(p => p.Institution)
            .WithMany()
            .HasForeignKey(p => p.InstitutionId);
        
        builder.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId);
    }
}
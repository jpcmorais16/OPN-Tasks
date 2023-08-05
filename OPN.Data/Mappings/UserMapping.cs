using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPN.Domain.Login;

namespace OPN.Data.Mappings;

public class UserMapping: IEntityTypeConfiguration<LoggedUser>
{
    public void Configure(EntityTypeBuilder<LoggedUser> builder)
    {
        builder.ToTable("Usuario");
        builder.HasKey(u => u.Idn);

        builder.Property(p => p.Idn).HasColumnName("Idn");
        builder.Property(p => p.Name).HasColumnName("Nome");
        builder.Property(p => p.CompletedTasks).HasColumnName("Completadas");
        builder.Property(p => p.CancelledTasks).HasColumnName("Canceladas");
        builder.Property(p => p.TaskId).HasColumnName("Task_id");

        builder.HasOne(p => p.Task)
            .WithOne()
            .HasForeignKey<LoggedUser>(p => p.TaskId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPN.Domain;

namespace OPN.Data.Mappings;

public class InstitutionMapping: IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable("instituicao");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Codigo");
        builder.Property(p => p.Name).HasColumnName("Nome");
    }
}
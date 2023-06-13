using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OPN.Domain;
using OPN.Domain.Login;
using OPN.Domain.Tasks;

namespace OPN.Data;

public class ApplicationContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationContext()
    {
            
    }
    public ApplicationContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoggedUser>().HasOne(s => s.Task)
            .WithOne()
            .HasForeignKey<OPNTask>(t => t.UserId);

        modelBuilder.Entity<OPNProductHandlingTask>().HasOne(t => t.Proportion)
            .WithOne();

        modelBuilder.Entity<Institution>().HasMany(i => i.Products)
            .WithMany(p => p.Institutions)
            .UsingEntity<InstitutionProportion>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_connectionString, ServerVersion.Parse("8.0.31"));
    }

    public DbSet<LoggedUser> LoggedUsers => Set<LoggedUser>();
    public DbSet<OPNProductHandlingTask> ProductHandlingTasks => Set<OPNProductHandlingTask>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Institution> Institutions => Set<Institution>();
    public DbSet<InstitutionProportion> InstitutionProportions => Set<InstitutionProportion>();
}

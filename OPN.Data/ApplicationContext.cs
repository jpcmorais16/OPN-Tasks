using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OPN.Domain;
using OPN.Domain.Login;
using OPN.Domain.Tasks;

namespace OPN.Data;

public class ApplicationContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_connectionString, ServerVersion.Parse("8.0.33"));
    }

    public DbSet<LoggedUser> LoggedUsers => Set<LoggedUser>();
    public DbSet<OPNProductHandlingTask> ProductHandlingTasks => Set<OPNProductHandlingTask>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Institution> Institutions => Set<Institution>();
    public DbSet<InstitutionProportion> InstitutionProportions => Set<InstitutionProportion>();
}

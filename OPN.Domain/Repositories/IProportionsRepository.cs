namespace OPN.Domain.Repositories;

public interface IProportionsRepository
{
    Task<InstitutionProportion?> GetRandomAvailableProportionAsync();
}

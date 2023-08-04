namespace OPN.Domain.Repositories;

public interface IProportionsRepository
{
    Task<InstitutionProportion> GetRandomAvailableProportionAsync();
    Task<InstitutionProportion> GetByKey((int, int) key);
}

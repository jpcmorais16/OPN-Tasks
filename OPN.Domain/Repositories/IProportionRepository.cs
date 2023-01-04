namespace OPN.Domain.Repositories;

public interface IProportionRepository
{
    Task<InstitutionProportion> GetRandomAvailableProportionAsync();
}

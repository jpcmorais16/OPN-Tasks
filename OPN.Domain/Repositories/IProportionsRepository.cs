namespace OPN.Domain.Repositories;

public interface IProportionsRepository
{
    Task<InstitutionProportion> GetByKey((int, int) key);
    Task<List<InstitutionProportion>> GetProportions();
}

namespace OPN.Domain.Repositories;

public interface IInstitutionRepository
{
    Task<Institution> GetByIdAsync(int institutionId);
}

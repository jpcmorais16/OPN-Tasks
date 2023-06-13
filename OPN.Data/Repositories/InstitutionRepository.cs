using Microsoft.EntityFrameworkCore;
using OPN.Domain;
using OPN.Domain.Repositories;

namespace OPN.Data.Repositories;

public class InstitutionRepository: IInstitutionRepository
{
    private readonly ApplicationContext _context;

    public InstitutionRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<Institution?> GetByIdAsync(int institutionId)
    {
        var institution = await _context.Institutions.FirstOrDefaultAsync(i => i.Id == institutionId);

        return institution;
    }
}
using Microsoft.EntityFrameworkCore;
using OPN.Domain;
using OPN.Domain.Repositories;

namespace OPN.Data.Repositories;

public class ProportionsRepository: IProportionsRepository
{
    private readonly ApplicationContext _context;
    public ProportionsRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<InstitutionProportion?> GetRandomAvailableProportionAsync()
    {
        var availableProportions = _context.InstitutionProportions.Where(p => p.Status == EProportionStatus.NotUsed)
            .Include(p => p.Institution)
            .Include(p => p.Product);
        
        var proportion = await availableProportions
                                    .FirstOrDefaultAsync();
        return proportion;
    }
}
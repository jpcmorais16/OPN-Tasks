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

    public Task<InstitutionProportion> GetByKey((int, int) key)
    {
        var proportion = _context.InstitutionProportions
            .Include(p => p.Product)
            .FirstOrDefaultAsync(
                p => p.ProductId == key.Item1 && p.InstitutionId == key.Item2);

        if (proportion == null)
            throw new Exception("Proporção não encontrada");

        return proportion!;
    }

    public async Task<List<InstitutionProportion>> GetProportions()
    {
        return await _context.InstitutionProportions.ToListAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using OPN.Domain;
using OPN.Domain.Repositories;

namespace OPN.Data.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly ApplicationContext _context;
    
    public ProductRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<Product?> GetByIdAsync(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        return product;
    }
}
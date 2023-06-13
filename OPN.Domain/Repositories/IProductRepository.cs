namespace OPN.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId);
}

using OPN.Domain;
using OPN.Domain.Repositories;

namespace OPN.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;
    public UnitOfWork(IUserRepository userRepository,
        IProductHandlingTasksRepository productHandlingTasksRepository,
        ApplicationContext context,
        IProductRepository productRepository,
        IInstitutionRepository institutionRepository,
        IProportionsRepository proportionRepository)
    {
        _context = context;
        UserRepository = userRepository;
        ProductHandlingTasksRepository = productHandlingTasksRepository;
        ProductRepository = productRepository;
        InstitutionRepository = institutionRepository;
        ProportionsRepository = proportionRepository;
    }
    public IUserRepository UserRepository { get; }

    public IProductHandlingTasksRepository ProductHandlingTasksRepository { get; }

    public IProductRepository ProductRepository { get; }

    public IInstitutionRepository InstitutionRepository { get; }

    public IProportionsRepository ProportionsRepository { get; }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}

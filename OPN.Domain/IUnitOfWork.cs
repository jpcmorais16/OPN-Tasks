using OPN.Domain.Repositories;

namespace OPN.Domain;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IProductHandlingTasksRepository ProductHandlingTasksRepository { get; }
    IProductRepository ProductRepository { get; }
    IInstitutionRepository InstitutionRepository { get; }
    IProportionsRepository ProportionsRepository { get; }

    Task CommitAsync();
}

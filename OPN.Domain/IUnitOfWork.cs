using OPN.Domain.Repositories;

namespace OPN.Domain;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IActiveProductHandlingTasksRepository ActiveProductHandlingTasksRepository { get; }
    IProductRepository ProductRepository { get; }
    IInstitutionRepository InstitutionRepository { get; }
    IProportionRepository ProportionRepository { get; }

    Task CommitAsync();
}

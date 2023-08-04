using OPN.Domain;
using OPN.Domain.Login;
using OPN.Domain.Tasks;
using OPN.Services.Interfaces;

namespace OPN.Services;
public class ProductHandlingTaskService: ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductHandlingTaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<OPNProductHandlingTask>> GetUserCompletedTasks(string idn)
    {
        return await _unitOfWork.ProductHandlingTasksRepository.GetUserCompletedTasks(idn);
    }

    public List<OPNProductHandlingTask> GetAllCompletedTasks()
    {
        throw new NotImplementedException();
    }

    public int GetUserNumberOfCompletedTasks(string idn)
    {
        throw new NotImplementedException();
    }

    public int GetNumberOfCompletedTasks()
    {
        throw new NotImplementedException();
    }
    public List<LoggedUser> GetRanking()
    {
        throw new NotImplementedException();
    }

    public async Task<OPNProductHandlingTask?> GetUserCurrentTask(string idn)
    {
        return await _unitOfWork.ProductHandlingTasksRepository.GetCurrentTask(idn);
    }

    public async Task CompleteTask(string idn)
    {
        var user =  await _unitOfWork.UserRepository.GetByIdn(idn);

        if (user == null)
            throw new Exception("Esse IDN não fez login!");

        var proportionKey = user.CompleteTask();

        var proportion = await _unitOfWork.ProportionsRepository.GetByKey(proportionKey);

        proportion!.Status = EProportionStatus.Used;

        _unitOfWork.UserRepository.UpdateUser(user);

        await _unitOfWork.CommitAsync();
    }

    public async Task<OPNProductHandlingTask> CreateRandomProductHandlingTask(string idn)
    {
        var user =   await _unitOfWork.UserRepository.GetByIdn(idn);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        var proportion =  await _unitOfWork.ProportionsRepository.GetRandomAvailableProportionAsync();
        
        var task = new OPNProductHandlingTask 
        {
            UserIDN = idn,
            Institution = proportion!.Institution,
            Product = proportion.Product,
            CreationTime = DateTime.UtcNow,
            ProportionKey = (proportion.ProductId, proportion.InstitutionId)
        };

        user.AddTask(task);

        proportion.Status = EProportionStatus.InUse;

        await _unitOfWork.ProductHandlingTasksRepository.RegisterTaskAsync(task);

        await _unitOfWork.CommitAsync();

        return task;
    }

    public async Task CancelTask(string idn)
    {
        var user = await _unitOfWork.UserRepository.GetByIdn(idn);

        if(user == null)
            throw new Exception("Este IDN não fez login!");

        var proportionKey = user.CancelTask();
        
        var proportion = await _unitOfWork.ProportionsRepository.GetByKey(proportionKey);

        proportion!.Status = EProportionStatus.NotUsed;

        await _unitOfWork.CommitAsync();
    }
}

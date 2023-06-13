using OPN.Domain;
using OPN.Domain.Login;
using OPN.Domain.Tasks;
using OPN.Services.Interfaces;
using OPN.Services.Requests;

namespace OPN.Services;
public class ProductHandlingTaskService: ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductHandlingTaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<OPNProductHandlingTask> GetUserCompletedTasks(string userIdn)
    {
        throw new NotImplementedException();
    }

    public List<OPNProductHandlingTask> GetAllCompletedTasks()
    {
        throw new NotImplementedException();
    }

    public int GetUserNumberOfCompletedTasks(string userIdn)
    {
        throw new NotImplementedException();
    }

    public int GetNumberOfCompletedTasks()
    {
        throw new NotImplementedException();
    }

    void ITaskService.CompleteTask(string userIdn)
    {
        throw new NotImplementedException();
    }

    void ITaskService.CancelTask(string idn)
    {
        throw new NotImplementedException();
    }

    public List<LoggedUser> GetRanking()
    {
        throw new NotImplementedException();
    }

    public async Task CompleteTask(string userIdn)
    {
        var user =  await _unitOfWork.UserRepository.GetByIdn(userIdn);

        if (user == null)
            throw new Exception("Esse IDN não fez login!");

        user.CompleteTask();

        _unitOfWork.UserRepository.UpdateUser(user);

        await _unitOfWork.CommitAsync();
    }

    public async Task<OPNProductHandlingTask> CreateRandomProductHandlingTask(TaskRequest request)
    {
        var user =   await _unitOfWork.UserRepository.GetByIdn(request.LoggedUserIDN);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        var proportion =  await _unitOfWork.ProportionsRepository.GetRandomAvailableProportionAsync();
        
        var task = new OPNProductHandlingTask 
        {
            UserIDN = request.LoggedUserIDN,
            Institution = proportion!.Institution,
            Product = proportion.Product,
            CreationTime = DateTime.UtcNow 
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

        user.CancelTask();

        var task = await _unitOfWork.ProductHandlingTasksRepository.GetByIdAsync(user.TaskId);
        task.Status = ETaskStatus.Cancelled;

        await _unitOfWork.CommitAsync();
    }
     
    //public List<OPNProductHandlingTask> GetAllCompletedTasks()
    //{
    //    var tasks = _taskFetcher.FetchProductHandlingTasks();

    //    return tasks.Where(t => t.ConclusionTime != null).ToList();
    //}

    //public List<OPNProductHandlingTask> GetUserCompletedTasks(string userIDN)
    //{
    //    var tasks = _taskFetcher.FetchProductHandlingTasks();

    //    return tasks.Where(t => t.ConclusionTime != null && t.UserIDN.Equals(userIDN)).ToList();
    //}

    //public int GetUserNumberOfCompletedTasks(string userIDN)
    //{
    //    //var tasks = _taskFetcher.FetchProductHandlingTasks();

    //    //return tasks.Where(t => t.ConclusionTime != null && t.UserIDN.Equals(userIDN)).ToList().Count;

    //    var user = _userDataFetcher.FetchUser(userIDN);

    //    return user.GetNumberOfCompletedTasks();
    //}

    //public int GetNumberOfCompletedTasks()
    //{
    //    var tasks = _taskFetcher.FetchProductHandlingTasks();

    //    return tasks.Where(t => t.ConclusionTime != null).ToList().Count;
    //}

    //public List<LoggedUser> GetRanking()
    //{
    //    var users = _userDataFetcher.FetchUsers();

    //    return users.OrderByDescending(u => u.CompletedTasks).ToList();
    //}
}

using OPN.Domain;
using OPN.Domain.Tasks;
using OPN.Services.Requests;

namespace OPN.Services;
public class ProductHandlingTaskService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductHandlingTaskService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task CompleteTask(string UserIDN)
    {
        var user = await _unitOfWork.UserRepository.GetByIdnAsync(UserIDN);

        if (user == null)
            throw new Exception("Esse IDN não fez login!");

        user.CompleteTask();

        await _unitOfWork.UserRepository.UpdateUserAsync(user);

        await _unitOfWork.CommitAsync();
    }

    public async Task<OPNProductHandlingTask> CreateRandomProductHandlingTask(TaskRequest request)
    {
        var user = await _unitOfWork.UserRepository.GetByIdnAsync(request.LoggedUserIDN);

        var proportion = await _unitOfWork.ProportionRepository.GetRandomAvailableProportionAsync();

        var product = await _unitOfWork.ProductRepository.GetByIdAsync(proportion.ProductId);
        var institution = await _unitOfWork.InstitutionRepository.GetByIdAsync(proportion.InstitutionId);

        var task = new OPNProductHandlingTask 
        {
            UserIdn = request.LoggedUserIDN,
            Institution = institution,
            Product = product,
            CreationTime = DateTime.UtcNow 
        };

        user.AddTask(task);

        await _unitOfWork.ActiveProductHandlingTasksRepository.RegisterTaskAsync(task);

        await _unitOfWork.CommitAsync();

        return task;
    }

    public async Task CancelTask(string IDN)
    {
        var user = await _unitOfWork.UserRepository.GetByIdnAsync(IDN);

        if(user == null)
            throw new Exception("Este IDN não fez login!");

        user.CancelTask();

        var task = await _unitOfWork.ActiveProductHandlingTasksRepository.GetByIdAsync();

        await _unitOfWork.ActiveProductHandlingTasksRepository.DeleteAsync(task);

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

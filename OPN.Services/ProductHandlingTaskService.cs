using OPN.Domain;
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
    
    public async Task CompleteTask(string idn)
    {
        var user =  await _unitOfWork.UserRepository.GetByIdn(idn);

        if (user == null)
            throw new Exception("Esse IDN não fez login!");

        var task = user.CompleteTask();

        var proportion = await _unitOfWork.ProportionsRepository.GetByKey((task.ProductId, task.InstitutionId));

        proportion.Status = EProportionStatus.Used;
        
        var product = proportion.Product;
        product!.CurrentAmount -= proportion.Value * product.InitialAmount;

        await _unitOfWork.CommitAsync();
    }
    
    public async Task CancelTask(string idn)
    {
        var user = await _unitOfWork.UserRepository.GetByIdn(idn);

        if(user == null)
            throw new Exception("Este IDN não fez login!");

        var task = user.CancelTask();

        var proportion = await _unitOfWork.ProportionsRepository.GetByKey((task.ProductId, task.InstitutionId));

        proportion!.Status = EProportionStatus.NotUsed;
        proportion!.Product!.CurrentAmount += task.Amount;

        await _unitOfWork.CommitAsync();
    }
}

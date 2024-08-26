using System.Net;
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

    public async Task<OPNProductHandlingTask> GetTaskToUser(string idn)
    {
        var user =  await _unitOfWork.UserRepository.GetByIdn(idn);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        if (user.Task != null)
            return (await _unitOfWork.ProductHandlingTasksRepository.GetCurrentTask(idn))!;

        var task = await _unitOfWork.ProductHandlingTasksRepository.GetRandomTask();
            
        user.AddTask(task);

        task.UserIdn = user.Idn;

        task.Status = ETaskStatus.InExecution;

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

        var product = proportion.Product;
        product!.CurrentAmount -= (int) (proportion.Value * product.InitialAmount / 100);

        await _unitOfWork.CommitAsync();
    }
    
    public async Task CancelTask(string idn)
    {
        var user = await _unitOfWork.UserRepository.GetByIdn(idn);

        if(user == null)
            throw new Exception("Este IDN não fez login!");
        
        user.CancelTask();

        await _unitOfWork.CommitAsync();
    }

    public async Task Reset()
    {
        await _unitOfWork.ProductRepository.Reset();

        await _unitOfWork.UserRepository.Reset();

        await _unitOfWork.ProductHandlingTasksRepository.Reset();

        await CreateAllTasksAsync();
        
        await _unitOfWork.CommitAsync();
    }

    private async Task CreateAllTasksAsync()
    {
        var proportions = await _unitOfWork.ProportionsRepository.GetProportions();

        var taskList = new List<OPNProductHandlingTask>();

        var taskId = 1;
        
        var institutionAmount = proportions.Select(p => p.Institution).Distinct().Count();
        var counter = 0;
        var currentProductAmount = proportions.First().Product!.InitialAmount;
        
        proportions.ForEach(proportion =>
        {

            int amount = 0;
            
            (currentProductAmount, counter, amount) =
                CalculateTaskProductAmount(proportion, counter, currentProductAmount, institutionAmount);
            
            if (amount == 0)
                return;

            while (amount > 20)
            {
                var otherTask = new OPNProductHandlingTask
                {
                    Id = taskId,
                    InstitutionId = proportion!.InstitutionId,
                    ProductId = proportion.ProductId,
                    CreationTime = DateTime.UtcNow,
                    Amount = 20,
                    Status = ETaskStatus.Waiting
                };
                    
                taskList.Add(otherTask);

                amount -= 20;
                taskId++;
            }
            
            var task = new OPNProductHandlingTask 
            {
                Id = taskId,
                InstitutionId = proportion!.InstitutionId,
                ProductId = proportion.ProductId,
                CreationTime = DateTime.UtcNow,
                Amount = amount,
                Status = ETaskStatus.Waiting
            };

            taskList.Add(task);
            taskId++;
        });

        await _unitOfWork.ProductHandlingTasksRepository.RegisterRangeAsync(taskList);
    }

    private (int, int, int) CalculateTaskProductAmount(InstitutionProportion proportion, int counter, int currentProductAmount, int institutionAmount)
    {
        if (counter == 0) currentProductAmount = proportion.Product!.InitialAmount;

        var amount = (int) Math.Ceiling(proportion.Value * proportion.Product!.InitialAmount / 100);
            
        if(counter % 2 == 1)
            amount = (int) Math.Floor(proportion.Value * proportion.Product!.InitialAmount / 100);

        if (counter++ > institutionAmount)
        {
            counter = 0;

            amount = currentProductAmount;
        }
        
        currentProductAmount -= amount;
        
        return (currentProductAmount, counter, amount);
    }
}

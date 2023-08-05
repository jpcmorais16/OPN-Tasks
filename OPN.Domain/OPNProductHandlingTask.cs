using OPN.Domain.Login;

namespace OPN.Domain.Tasks;
public class OPNProductHandlingTask
{
    public int Id { get; set; }
    public string UserIDN { get; set; }
    public int Amount { get; set; }
    public ETaskStatus Status { get; set; } = ETaskStatus.InExecution;
    public DateTime CreationTime { get; set; }
    public DateTime? ConclusionTime { get; set; }
    public DateTime? CancelTime { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; init; }
    public int InstitutionId { get; set; }
    public Institution? Institution { get; init; }
}

public enum ETaskStatus
{
    InExecution = 1,
    Completed = 2,
    Cancelled = 3
}
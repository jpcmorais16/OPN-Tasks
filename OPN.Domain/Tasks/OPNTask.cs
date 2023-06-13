namespace OPN.Domain.Tasks;
public abstract class OPNTask
{
    public int Id { get; set; }
    public string Goal { get; set; }
    public int UserId { get; set; }
    public string UserIDN { get; set; }
    public ETaskStatus Status { get; set; } = ETaskStatus.InExecution;
    public DateTime CreationTime { get; set; }
    public DateTime? ConclusionTime { get; set; }
}

public enum ETaskStatus
{
    InExecution = 1,
    Finished = 2,
    Cancelled = 3
}
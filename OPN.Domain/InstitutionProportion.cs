using OPN.Domain.Tasks;

namespace OPN.Domain;

public class InstitutionProportion
{
    public float Value { get; set; }
    public EProportionStatus Status { get; set; }
    public int InstitutionId { get; set; }
    public Institution? Institution { get; set; }
    public int ProductId  { get; set; }
    public Product? Product { get; set; }

    public InstitutionProportion()
    {
            
    }
}

public enum EProportionStatus
{
    NotUsed = 0,
    InUse = 1,
    Used = 2
}

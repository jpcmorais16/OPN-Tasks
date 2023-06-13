using OPN.Domain.Login;

namespace OPN.Domain.Tasks;
public class OPNProductHandlingTask : OPNTask
{
    public int ProductId { get; set; }
    public Product? Product { get; init; }
    public int InstitutionId { get; set; }
    public Institution? Institution { get; init; }
    public int ProportionId { get; set; }
    public InstitutionProportion? Proportion { get; init; }

    public string GetGoal()
    {
        return $"Levar {Proportion!.Value * Product!.InitialAmount} de {Product.Name} para {Institution!.Name}";
    }
}

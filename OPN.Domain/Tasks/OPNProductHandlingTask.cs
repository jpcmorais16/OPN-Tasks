using OPN.Domain.Login;

namespace OPN.Domain.Tasks;
public class OPNProductHandlingTask : OPNTask
{
    public OPNProductHandlingTask()
    {

    }
    public OPNProductHandlingTask(string userIdn, Product product, Institution institution, InstitutionProportion proportion)
    {
        UserIdn = userIdn;
        Product = product;
        Institution = institution;
        Proportion = proportion; 
    }
    public string UserIdn { get; set; }
    public Product Product { get; set; }
    public Institution Institution { get; set; }
    public InstitutionProportion Proportion { get; set; }

    public string GetGoal()
    {
        return $"Levar {Proportion.Value * Product.InitialAmount} de {Product.Name} para {Institution.Name}";
    }
}

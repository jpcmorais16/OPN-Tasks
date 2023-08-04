using OPN.Domain.Tasks;

namespace OPN.Domain;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int InitialAmount { get; set; }
    public int CurrentAmount { get; set; }
    
    public Product()
    {
            
    }
}

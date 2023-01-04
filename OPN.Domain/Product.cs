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

    public Product(Dictionary<string, int> institutionProportions, int initialAmount)
    {
        InitialAmount = initialAmount;
        CurrentAmount = initialAmount;
    }

    public void RemoveFromProduct(int amount)
    {
        CurrentAmount -= amount;
    }
}

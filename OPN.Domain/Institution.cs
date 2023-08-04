using OPN.Domain.Tasks;

namespace OPN.Domain;
public class Institution
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Institution()
    {
            
    }

    public Institution(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

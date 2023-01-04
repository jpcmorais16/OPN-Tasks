namespace OPN.Domain;

public class InstitutionProportion
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int InstitutionId { get; set; }
    public int Value { get; set; }
    public bool AlreadyUsed { get; set; }
}

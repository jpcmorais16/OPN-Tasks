namespace OPN.Domain.Tasks
{
    public abstract class OPNTask
    {
        public int Id { get; set; }
        public string Goal { get; set; }
        public string UserIDN { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ConclusionTime { get; set; }
    }
}
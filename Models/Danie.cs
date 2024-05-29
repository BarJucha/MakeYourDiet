namespace AuthenticationApp.Models
{
    public class Danie
    {
        public int DanieId { get; set; }
        public string Nazwa { get; set; }
        public ICollection<SkladDania> ? SkladDania { get; set; }
        public ICollection<PlanDnia> ? PlanDnia { get; set; }
    }
}

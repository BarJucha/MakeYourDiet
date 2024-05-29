namespace AuthenticationApp.Models
{
    public class PlanDnia
    {
        public int PlanDniaId { get; set; }
        public DateTime DataDnia { get; set; }

        public Uzytkownik Uzytkownik { get; set; }
        public Danie Danie { get; set; }
    }
    public class PlanDniaGroupViewModel
    {
        public DateTime Date { get; set; }
        public List<PlanDnia> Plany { get; set; }
    }
}

namespace AuthenticationApp.Models
{
    public class Produkt
    {
        public int ProduktId { get; set; }
        public string Nazwa { get; set; }
        public int Kalorycznosc { get; set; }
        public int Weglowodany { get; set; }
        public int Tluszcze { get; set; }
        public int Bialka { get; set; }
        public ICollection<SkladDania> ?SkladDania{ get; set; }
    }
}

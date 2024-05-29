namespace AuthenticationApp.Models
{
    public class SkladDania
    {
        public int SkladDaniaId { get; set;}
        public int Ilosc { get; set; }
        
        public Danie Danie { get; set; }
        public Produkt Produkt { get; set; }
    }
}

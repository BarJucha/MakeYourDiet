using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationApp.Models
{
    public class Uzytkownik
    {
        public int UzytkownikId { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public bool CzyAdmin { get; set; }
        public ICollection<PlanDnia> ?PlanDnia { get; set; }
        public void SetHashedPassword(string password)
        {
            // Tworzenie obiektu MD5
            using (MD5 md5 = MD5.Create())
            {
                // Konwertowanie hasła na tablicę bajtów
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);

                // Obliczanie skrótu MD5 hasła
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Konwertowanie tablicy bajtów na string hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                // Ustawienie zahaszowanego hasła
                Haslo = sb.ToString();
            }
        }
        private string GetHashedPassword(string password)
        {
            // Tworzenie obiektu MD5
            using (MD5 md5 = MD5.Create())
            {
                // Konwertowanie hasła na tablicę bajtów
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);

                // Obliczanie skrótu MD5 hasła
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Konwertowanie tablicy bajtów na string hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                // Zwrócenie zahaszowanego hasła
                return sb.ToString();
            }
        }
    }
    
}
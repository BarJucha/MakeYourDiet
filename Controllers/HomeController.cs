using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models;
using AuthenticationApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Projekt.Data;
using System.Security.Cryptography;
using System.Text;

namespace Projekt.Controllers;

public class HomeController : Controller
{
    private readonly ProjektContext _context;

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, ProjektContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
        public IActionResult CheckLogin(string username, string password)
        {
            string hashedPass = SetHashedPassword(password);
            var user = _context.Uzytkownik.FirstOrDefault(u => u.Login == username && u.Haslo == hashedPass);
            if (user != null)
            {
                if (user.CzyAdmin)
                {
                    HttpContext.Session.SetString("Admin", username);
                }
                HttpContext.Session.SetString("LoggedInUser", username);
                HttpContext.Session.SetInt32("UserID", user.UzytkownikId);
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ViewBag.Message = "Incorrect login or password!";
                return View("Index");
            }
        }

        public IActionResult LoggedIn()
        {
            var username = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LoggedInUser");
            HttpContext.Session.Remove("UserID");
            if (HttpContext.Session.GetString("Admin") != null)
            {
                HttpContext.Session.Remove("Admin");
            }
            return RedirectToAction("Index");
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public static string SetHashedPassword(string password)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}

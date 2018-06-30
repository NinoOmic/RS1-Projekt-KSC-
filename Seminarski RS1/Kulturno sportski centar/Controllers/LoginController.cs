using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Provjera(string username, string password)
        {
            using (MojContext db = new MojContext())
            {
                Korisnik Korisnik = db.Korisnik.Include(x=>x.Osoba).Where(x => x.Osoba.KorisnickoIme == username).FirstOrDefault();
                if (password==""||username=="")
                {
                    return RedirectToAction("Index", "Login");

                }
                else if (Korisnik.Osoba.Lozinka == password)
                {
                    Autentifikacija.KorisnikSesija = Korisnik;
                    return RedirectToAction("Index", "Home");
                }
                                   
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }
    }
}
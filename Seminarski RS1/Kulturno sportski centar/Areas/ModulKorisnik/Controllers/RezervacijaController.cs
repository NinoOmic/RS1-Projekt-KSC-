using Kulturno_sportski_centar.Areas.ModulKorisnik.Models;
using Kulturno_sportski_centar.DAL;
using Kulturno_sportski_centar.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik.Controllers
{
    public class RezervacijaController : Controller
    {
        MojContext ctx = new MojContext();
        public ActionResult Prikazi()
        {
            RezervacijaPrikaziViewModel Model = new RezervacijaPrikaziViewModel();
            Model.Termini = ctx.Termin.ToList();
            Model.Korisnici = ctx.Korisnik.ToList();
            ProvjeraRezervacija();
            Model.Rezervacije = ctx.Rezervacija
                .Select(x => new RezervacijaPrikaziViewModel.RezervacijaInfo
                {
                    Id = x.Id,
                    Datum = x.Termin.Datum,
                    Pocetak = x.Termin.Pocetak,
                    Kraj = x.Termin.Kraj,
                    Korisnik = x.Korisnik.Osoba.KorisnickoIme,
                    Sala = x.Termin.Sala.Naziv,
                    Zavrsena = x.Zavrsena
                }).ToList();
            return View("Prikazi",Model);
        }
        private void ProvjeraRezervacija()
        {
            List<Rezervacija> rezervacijas = ctx.Rezervacija.ToList();
            DateTime Datum = DateTime.Today;
            foreach (var x in rezervacijas)
            {
                if (x.Termin.Datum < Datum)
                    x.Zavrsena = true;
            }
            ctx.SaveChanges();
        }
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Obrisi(int Id)
        {
            
            Rezervacija R = new Rezervacija();
            R = ctx.Rezervacija.Where(x => x.Id == Id).FirstOrDefault();
            ctx.Rezervacija.Remove(R);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

        public ActionResult Dodaj(int TerminId)
        {
            RezervacijaDodajModelView Model = new RezervacijaDodajModelView();
            Model.TerminId = TerminId;
            return View("Dodaj", Model);
        }
        public ActionResult Snimi(int TerminId)
        {
            Rezervacija R = new Rezervacija();
            R.Korisnik = Autentifikacija.KorisnikSesija;
            Termin T = ctx.Termin.Where(x => x.Id == TerminId).FirstOrDefault();
            T.Rezervisan = true;
            R.Termin = T;
            R.Zavrsena = false;
            ctx.Rezervacija.Add(R);
            ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
    }
}
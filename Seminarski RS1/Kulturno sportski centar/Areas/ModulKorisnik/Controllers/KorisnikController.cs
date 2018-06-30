using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.Entity;
using Kulturno_sportski_centar.Models;
using Kulturno_sportski_centar.Areas.ModulKorisnik.Models;
using static Kulturno_sportski_centar.Areas.ModulKorisnik.Models.KorisnikPrikaziViewModel;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik
{
    public class KorisnikController : Controller
    {
        MojContext ctx = new MojContext();

        public ActionResult Prikazi(int? UlogaNaSistemuId)
        {
            KorisnikPrikaziViewModel Model = new KorisnikPrikaziViewModel();
           
            Model.Korisnici = ctx.Korisnik
                  .Where(x => !UlogaNaSistemuId.HasValue || UlogaNaSistemuId == x.UlogaNaSistemuId)
                  .Select(x => new KorisnikPrikaziViewModel.KorisnikInfo()
                  {
                      Ime = x.Osoba.Ime,
                      Prezime = x.Osoba.Prezime,
                      KorisnickoIme = x.Osoba.KorisnickoIme,
                      UlogaNaSistemu = x.UlogaNaSistemu.Uloga,
                      Id = x.Id
                  }
                   ).ToList();
               Model.UlogeNaSistemu = ctx.UlogaNaSistemu.ToList();
             return View("Prikazi",Model);
        }
    

        public ActionResult Dodaj()
        {
            KorisnikEditViewModel Model = new KorisnikEditViewModel();
            Korisnik k = new Korisnik();
           k.Osoba = new Osoba();
            k.UlogaNaSistemu = new UlogaNaSistemu();
            Model.Uloge = UcitajUloge();
            Model.Gradovi = UcitajGradove();
            return View("Dodaj", Model);
        }
       

        public ActionResult index()
        {
            return View("index");
        }
        public ActionResult Detalji(int Id)
        {
            KorisnikPrikaziDetaljnoViewModel Model = new KorisnikPrikaziDetaljnoViewModel();
            
            List<Korisnik> Korisnici = ctx.Korisnik
           .Include(x => x.Osoba)
           .Include(x => x.Osoba.Grad)
           .Include(x => x.Osoba.Grad.Regija)
           .Include(x => x.Osoba.Grad.Regija.Drzava)
           .Include(x => x.UlogaNaSistemu)
           .ToList();

            Model = Korisnici.Where(x => x.Id == Id).Select(x => new KorisnikPrikaziDetaljnoViewModel()
            {
                Id = x.Id,
                Ime = x.Osoba.Ime,
                Prezime = x.Osoba.Prezime,
                KorisnickoIme = x.Osoba.KorisnickoIme,
                UlogaNaSistemu = x.UlogaNaSistemu.Uloga,
                DatumRegistracije = x.DatumRegistracije,
                DatumRodjenja = x.Osoba.DatumRodjenja,
                Grad = x.Osoba.Grad.Naziv,
                Adresa = x.Osoba.Adresa,
                JMBG = x.Osoba.JMBG,
                Email = x.Osoba.Email,
                Telefon = x.Osoba.Telefon
            }).FirstOrDefault();

            

            return View("Detalji",Model);
        }

        public ActionResult Uredi(int Id)
        {   Korisnik a = ctx.Korisnik.Where(x => x.Id == Id).Include(x=>x.Osoba).FirstOrDefault();
            KorisnikEditViewModel Model = new KorisnikEditViewModel();
            Model.Gradovi = UcitajGradove();
            Model.Uloge = UcitajUloge();
             Model.Ime=  a.Osoba.Ime;
            Model.DatumRegistracije= a.DatumRegistracije  ;
          Model.Prezime=  a.Osoba.Prezime ;
          Model.DatumRodjenja=  a.Osoba.DatumRodjenja;
           Model.KorisnckoIme= a.Osoba.KorisnickoIme ;
            Model.JMBG=a.Osoba.JMBG ;
          Model.Telefon=  a.Osoba.Telefon ;
            Model.Adresa=a.Osoba.Adresa ;
           Model.Email= a.Osoba.Email ;
           Model.Lozinka= a.Osoba.Lozinka;
           Model.GradId= a.Osoba.GradId ;
          Model.UlogaId= a.UlogaNaSistemuId ;
           

            return View("Dodaj",Model);
        }
        public ActionResult Spremi(KorisnikEditViewModel K)
        {
            if (!ModelState.IsValid)
            {
                K.Gradovi = UcitajGradove();
                K.Uloge = UcitajUloge();
                return View("Dodaj", K);
            }
            Korisnik korisnik;
            if ( K.Id==0)
            {
                korisnik = new Korisnik();
                korisnik.Osoba = new Osoba();
               //a.UlogaNaSistemu = new UlogaNaSistemu();
                ctx.Korisnik.Add(korisnik);
            }
            else
            {
                korisnik = ctx.Korisnik.Where(x => x.Id == K.Id).Include(x=>x.Osoba).FirstOrDefault();
              
            }

            korisnik.Osoba.Ime = K.Ime;
            korisnik.DatumRegistracije = DateTime.Now.Date;
            korisnik.Osoba.Prezime = K.Prezime;
            korisnik.Osoba.DatumRodjenja = K.DatumRodjenja;
            korisnik.Osoba.KorisnickoIme = K.KorisnckoIme;
            korisnik.Osoba.JMBG = K.JMBG;
            korisnik.Osoba.Telefon = K.Telefon;
            korisnik.Osoba.Adresa = K.Adresa;
            korisnik.Osoba.Email = K.Email;
            korisnik.Osoba.Lozinka = K.Lozinka;
            korisnik.Osoba.GradId = K.GradId;
            korisnik.UlogaNaSistemuId = K.UlogaId;
            korisnik.UlogaNaSistemu = ctx.UlogaNaSistemu.Where(x => x.Id == K.UlogaId).FirstOrDefault();
            korisnik.Osoba.Grad = ctx.Grad.Where(x => x.Id == K.GradId).FirstOrDefault();
             ctx.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        public ActionResult Obrisi(int Id)
        {
            Korisnik k = new Korisnik();
            k = ctx.Korisnik.Where(x => x.Id == Id).FirstOrDefault();
            List<Rezervacija> rezervacijas = ctx.Rezervacija.Where(x => x.Korisnik.Id == k.Id).ToList();
            foreach (var x in rezervacijas)
            {
                ctx.Rezervacija.Remove(x);
                ctx.SaveChanges();
            }
            ctx.Korisnik.Remove(k);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
        private List<SelectListItem> UcitajUloge()
        {
            List<SelectListItem> uloge = new List<SelectListItem>();
            uloge.AddRange(ctx.UlogaNaSistemu.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Uloga }));

            return uloge;
        }
        private List<SelectListItem> UcitajGradove()
        {
            List<SelectListItem> uloge = new List<SelectListItem>();
            uloge.AddRange(ctx.Grad.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }));

            return uloge;
        }
    }
}
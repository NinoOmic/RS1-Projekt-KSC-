using Kulturno_sportski_centar.Areas.ModulAdministrator.Models;
using Kulturno_sportski_centar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Controllers
{
    public class DrzavaController : Controller
    {
        MojContext ctx = new MojContext();
        public ActionResult Prikazi()
        {
            DrzavaPrikaziViewModel Model = new DrzavaPrikaziViewModel();
           Model.Drzave = ctx.Drzava.ToList();            
            return View("Prikazi",Model);
        }
        public ActionResult index()
        {
            return View("index");
        }
        public ActionResult Dodaj()
        {
            DrzavaEditModelView Model = new DrzavaEditModelView();
            Drzava d = new Drzava();

            d.Naziv = Model.Naziv;
            d.Oznaka = Model.Oznaka;
            return View("Dodaj",Model);
        }

        public ActionResult Uredi(int Id)
        {
            Drzava d = ctx.Drzava.Where(x => x.Id == Id).FirstOrDefault();
            DrzavaEditModelView Model = new DrzavaEditModelView();

             Model.Naziv = d.Naziv;
            Model.Oznaka= d.Oznaka;
            
            return View("Dodaj",Model);
        }

        public ActionResult Spremi(DrzavaEditModelView D)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj", D);
            }
            Drzava d;
            if (D.Id==0 )
            {
                d = new Drzava();
                
                ctx.Drzava.Add(d);
            }
            else
            {
                d = ctx.Drzava.Where(x => x.Id == D.Id).FirstOrDefault();
            }
      
            d.Naziv = D.Naziv;
            d.Oznaka = D.Oznaka;
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }
        public ActionResult Obrisi(int Id)
        {
            Drzava d = new Drzava();
            d = ctx.Drzava.Where(x => x.Id == Id).FirstOrDefault();
           
            ctx.Drzava.Remove(d);
            ctx.SaveChanges();

            return RedirectToAction("Prikazi");
        }

    }
}
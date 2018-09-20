using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Kulturno_sportski_centar.Areas.ModulZaposlenik.Models
{
    public class DodajOpremuVM
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Kolicina { get; set; }
        public int SalaId { get; set; }
        public Sala Sala { get; set; }
    }
}
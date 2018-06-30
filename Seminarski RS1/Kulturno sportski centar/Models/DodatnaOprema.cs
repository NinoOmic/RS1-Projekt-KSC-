using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class DodatnaOprema:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public string Naziv { get; set; }
        public string Opis { get; set; }
        
        //public int RezervacijaId { get; set; }
        //public Rezervacija Rezervacija { get; set; }
        public float Cijena { get; set; }
    }
}
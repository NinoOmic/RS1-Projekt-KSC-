using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Ocjena:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public int OcjenaBroj { get; set; }
        
        public string Komentar { get; set; }
        
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public int UslugaId { get; set; }
        public Usluga Usluga { get; set; }

    }
}
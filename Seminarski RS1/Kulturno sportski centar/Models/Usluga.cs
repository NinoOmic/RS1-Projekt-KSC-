    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Usluga:IEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public string Naziv { get; set; }
        public string Opis { get; set; }

        public float Cijena { get; set; }
        public Uposlenik Uposlenik { get; set; }
    }
}
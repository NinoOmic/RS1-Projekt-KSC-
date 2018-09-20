using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kulturno_sportski_centar.Areas.ModulAdministrator.Models
{
    public class UrediUposlenikVM
    {
        public int UposlenikId { get; set; }
        public string Zvanje { get; set; }
        public DateTime DatumZaposljenja { get; set; }
        public int RadnoMjestoId{get;set;}
        public List<SelectListItem> RadnaMjesta { get; set; }
    }
}
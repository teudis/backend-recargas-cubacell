using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class SearchInputModelView
    {

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string UserId { get; set; }       
        public string PerfilRecarga { get; set; }
        public string EmailTarget { get; set; }
        public string PhoneTarget { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class RecargaNautaModelView
    {
       [ Required, EmailAddress]
        public string EmailAddressTarget { get; set; }

        public int Id{ get; set; }
    }
}

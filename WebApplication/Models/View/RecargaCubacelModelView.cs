using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class RecargaCubacelModelView
    {

        [Required]
        public string PhoneNumberTarget { get; set; }

        public int Id { get; set; }
    }
}

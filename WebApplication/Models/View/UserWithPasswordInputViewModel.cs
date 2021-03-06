using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class UserWithPasswordInputViewModel : UserInputViewModel   {
        
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas tienen que coincidir.")]
        public string ConfirmPassword { get; set; }

        public override User Export()
        {
            var model = base.Export();
            model.PasswordHash = Password;            
            return model;
        }

      

    }
}

using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class UserDisplayViewModel : IEntityDisplayViewModel<User, string>
    {
        public string Id { get ; set ; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telefono")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        public void Import(User entity)
        {
            Id = entity.Id;
            Email = entity.Email;            
            PhoneNumber = entity.PhoneNumber;
            FullName = entity.FullName;
        }
    }
}

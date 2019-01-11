using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class UserInputViewModel : IEntityInputViewModel<User, string>
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

       
        public string PhoneNumber { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        public virtual User Export()
        {
            var entity = new User();
            Mergue(entity);
            return entity;
        }

        public void Import(User entity)
        {
            Id = entity.Id;
            Email = entity.Email;            
            PhoneNumber = entity.PhoneNumber;
            FullName = entity.FullName;
        }

        public void Mergue(User entity)
        {
            entity.Email = Email;           
            entity.PhoneNumber = PhoneNumber;
            entity.FullName = FullName;
        }
    }
}

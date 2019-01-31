using Microsoft.AspNetCore.Mvc.Rendering;
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

        public string UserName { get; set; }

        public string NormalizedEmail { get; set; }

        public string NormalizedUserName { get; set; }
        public bool LockoutEnabled { get; set; }

        public string SecurityStamp { get; set; }

        [Display(Name = "Roles")]
        public string IdRole { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

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
            Email = entity.UserName;            
            NormalizedEmail = entity.NormalizedEmail;
            NormalizedUserName = entity.NormalizedUserName;
            LockoutEnabled = entity.LockoutEnabled;
            SecurityStamp = entity.SecurityStamp;
            IdRole = entity.IdRole;

        }

        public void Mergue(User entity)
        {
            entity.Email = Email;           
            entity.PhoneNumber = PhoneNumber;
            entity.FullName = FullName;
            entity.UserName = Email;
            NormalizedEmail = Email.ToUpper();
            entity.NormalizedEmail = NormalizedEmail;
            entity.NormalizedUserName = NormalizedEmail;
            entity.LockoutEnabled = true;
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.IdRole = IdRole;
        }
    }
}

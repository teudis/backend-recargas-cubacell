using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class AccountInputViewModel : IEntityInputViewModel<Account,Guid>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Habilitado")]
        public bool Enabled { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Monto")]
        public float Balance { get; set; }

        public Account Export()
        {
            var entity = new Account();
            Mergue(entity);
            return entity;
        }

        public void Import(Account entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Balance = entity.Balance;
            Enabled = entity.Enabled;
        }

        public void Mergue(Account entity)
        {
            entity.Name = Name;
            entity.Balance = Balance;
            entity.Enabled = Enabled;
        }
    }
}

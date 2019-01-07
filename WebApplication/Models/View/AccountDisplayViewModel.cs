using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class AccountDisplayViewModel : IEntityDisplayViewModel<Account, Guid>
    {        

        public Guid Id { get; set; }       
        [Display(Name = "Habilitado")]
        public bool Enabled { get; set; }
        
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        
        [Display(Name = "Monto")]
        public float Balance { get; set; }

        public void Import(Account entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Balance = entity.Balance;
            Enabled = entity.Enabled;
        }
    }
}

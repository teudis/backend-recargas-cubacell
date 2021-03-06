using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class CellularBalanceTuneUpRecordDisplayViewModel : IEntityDisplayViewModel<CellularBalanceTuneUpRecord, long>
    {
        public long Id { get ; set ; }

        [Display(Name = "Fecha de Creado")]
        public System.DateTime Created { get; private set; }

        [Display(Name = "Creado por")]
        public User Agent { get; set; }

        [Display(Name = "Monto")]
        public float Amount { get; set; }

        [Required, Phone]
        [Display(Name = "Telefono")]
        public string PhoneNumberTarget { get; set; }

        public void Import(CellularBalanceTuneUpRecord entity)
        {
            Id = entity.Id;
            Created = entity.Created;
            Agent = entity.Agent;
            Amount = entity.Amount;
            PhoneNumberTarget = entity.PhoneNumberTarget;
        }
    }
}

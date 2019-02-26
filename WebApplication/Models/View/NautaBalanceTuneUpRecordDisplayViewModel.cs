using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class NautaBalanceTuneUpRecordDisplayViewModel : IEntityDisplayViewModel<NautaBalanceTuneUpRecord, long>
    {
        public long Id { get; set ; }

        public System.DateTime Created { get; private set; }

        public User Agent { get; set; }

        public float Amount { get; set; }
        
        public string EmailAddressTarget { get; set; }

        public void Import(NautaBalanceTuneUpRecord entity)
        {
            Id = entity.Id;
            Created = entity.Created;
            Agent = entity.Agent;
            Amount = entity.Amount;
            EmailAddressTarget = entity.EmailAddressTarget;
        }
    }
}

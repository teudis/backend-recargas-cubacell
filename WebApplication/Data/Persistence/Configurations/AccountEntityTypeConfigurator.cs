using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Configurations
{
    public class AccountEntityTypeConfigurator: AbstractEntityTypeConfigurator<Entities.Account> 
    {
        protected override string GetTableName() => "accounts";
    }
}

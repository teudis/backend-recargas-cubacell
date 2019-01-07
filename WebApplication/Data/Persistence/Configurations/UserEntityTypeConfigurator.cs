using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Configurations
{
    public class UserEntityTypeConfigurator : AbstractEntityTypeConfigurator<Entities.User>
    {
        protected override string GetTableName() => "users";
    }
}

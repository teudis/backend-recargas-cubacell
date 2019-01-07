using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Configurations
{
    public abstract class AbstractEntityTypeConfigurator<TEntity> : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Configurations.AbstractEntityTypeConfigurator<TEntity>
        where TEntity : class
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Configurations;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Context;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Repositories;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Modules
{
    public class CubansConexionBusinessCommonApplicationModule : SmartSolucionesCuba.SAPRESSC.Core.Web.Common.IApplicationModule
    {
        public IDbContextDataInitialiser GetDbContextDataInitialiser()
        {
            return null;
        }

        public ICollection<IEntityTypeConfigurator> GetEntityTypeConfigurators()
        {
            return new System.Collections.Generic.List<IEntityTypeConfigurator>
            {
                new Persistence.Configurations.AccountEntityTypeConfigurator()
            };
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<Account, System.Guid>, AccountRepository>();
        }
    }
}

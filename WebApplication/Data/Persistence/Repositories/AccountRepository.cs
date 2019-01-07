using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Context;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Repositories
{
    public class AccountRepository : AbstractEntityRepository<Account, System.Guid>
    {
        public AccountRepository(AbstractTraceableDbContext dbContext) : base(dbContext)
        {
        }
    }
}

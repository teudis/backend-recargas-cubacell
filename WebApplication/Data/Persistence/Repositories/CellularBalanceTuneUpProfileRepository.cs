using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Repositories
{
    public class CellularBalanceTuneUpProfileRepository : AbstractEntityRepository<Entities.CellularBalanceTuneUpProfile, int>
    {
        public CellularBalanceTuneUpProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

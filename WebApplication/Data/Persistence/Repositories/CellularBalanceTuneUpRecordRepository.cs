using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Repositories
{
    public class CellularBalanceTuneUpRecordRepository : AbstractEntityRepository<Entities.CellularBalanceTuneUpRecord, long>
    {
        public CellularBalanceTuneUpRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

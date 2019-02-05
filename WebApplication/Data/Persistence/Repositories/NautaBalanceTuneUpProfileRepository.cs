using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Repositories
{
    public class NautaBalanceTuneUpProfileRepository : AbstractEntityRepository<Entities.NautaBalanceTuneUpProfile, int>
    {
        public NautaBalanceTuneUpProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Repositories
{
    public class AccountRepository : AbstractEntityRepository<Entities.Account, System.Guid>
    {
        public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

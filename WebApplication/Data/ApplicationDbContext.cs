using Microsoft.EntityFrameworkCore;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data
{
    public class ApplicationDbContext : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Context.AbstractTraceableIdentityDbContext<User>
    {
        public DbSet<CellularBalanceTuneUpProfile> CellularBalanceTuneUpProfiles { get; set; }
        public DbSet<CellularBalanceTuneUpRecord> CellularBalanceTuneUpRecords { get; set; }
        public DbSet<CellularBalanceTuneUpRequest> CellularBalanceTuneUpRequests { get; set; }
        public DbSet<NautaBalanceTuneUpProfile> NautaBalanceTuneUpProfiles { get; set; }
        public DbSet<NautaBalanceTuneUpRecord> NautaBalanceTuneUpRecords { get; set; }
        public DbSet<NautaBalanceTuneUpRequest> NautaBalanceTuneUpRequests { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Account>().ToTable("Accounts");
        }

        public override int SaveChanges()
        {
            return SaveChanges("framework");
        }
    }
}

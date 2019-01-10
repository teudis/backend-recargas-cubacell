using Microsoft.EntityFrameworkCore;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data
{
    public class ApplicationDbContext : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Context.AbstractTraceableIdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

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

            new Persistence.Configurations.AccountEntityTypeConfigurator().Configure(builder, Database.GetDbConnection().Database);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.Relational().TableName.ToLower();

                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToLower();
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToLower();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToLower();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToLower();
                }
            }
        }

        public override int SaveChanges()
        {
            return SaveChanges("framework");
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System.Linq;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Trace> Traces { get; set; }

        public DbSet<CellularBalanceTuneUpProfile> CellularBalanceTuneUpProfiles { get; set; }
        public DbSet<CellularBalanceTuneUpRecord> CellularBalanceTuneUpRecords { get; set; }
        public DbSet<CellularBalanceTuneUpRequest> CellularBalanceTuneUpRequests { get; set; }
        public DbSet<NautaBalanceTuneUpProfile> NautaBalanceTuneUpProfiles { get; set; }
        public DbSet<NautaBalanceTuneUpRecord> NautaBalanceTuneUpRecords { get; set; }
        public DbSet<NautaBalanceTuneUpRequest> NautaBalanceTuneUpRequests { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            return SaveChanges("framework");
        }

        public int SaveChanges(string author)
        {
            var relevantEntryChanges = ChangeTracker.Entries().Where(changeEntry => !(changeEntry.Entity is Trace) && (changeEntry.State == EntityState.Added || changeEntry.State == EntityState.Deleted || changeEntry.State == EntityState.Modified)).ToList();

            var changesTypes = new System.Collections.Generic.List<EntityState>();
            var beforeDatas = new System.Collections.Generic.List<string>();

            foreach (var changeEntry in relevantEntryChanges)
            {
                changesTypes.Add(changeEntry.State);
                beforeDatas.Add(Newtonsoft.Json.JsonConvert.SerializeObject(changeEntry.OriginalValues.ToObject()));
            };

            base.SaveChanges();

            var counter = 0;

            foreach (var changeEntry in relevantEntryChanges)
            {
                var trace = new Trace
                {
                    Type = changesTypes[counter],
                    Author = author,
                    EntityName = changeEntry.Metadata.Name,
                    BeforeData = beforeDatas[counter],
                    AfterData = Newtonsoft.Json.JsonConvert.SerializeObject(changeEntry.CurrentValues.ToObject())
                };

                Trace lastTrace = null;

                if (Traces.LongCount() > 0)
                {
                    lastTrace = Traces.Last();
                }

                Traces.Add(trace);

                base.SaveChanges();

                trace.Signature = SmartSolucionesCuba.SAPRESSC.Core.Persistence.Helpers.DataIntegrityHelper.GetEntitySignature(trace);

                Traces.Update(trace);

                base.SaveChanges();

                if (lastTrace != null)
                {
                    lastTrace.NextId = trace.Id;

                    lastTrace.Signature = SmartSolucionesCuba.SAPRESSC.Core.Persistence.Helpers.DataIntegrityHelper.GetEntitySignature(lastTrace);

                    Traces.Update(lastTrace);

                    base.SaveChanges();
                }

                counter++;
            }

            return base.SaveChanges();
        }
    }
}

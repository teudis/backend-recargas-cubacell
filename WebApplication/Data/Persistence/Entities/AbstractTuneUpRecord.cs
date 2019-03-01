using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractTuneUpRecord : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<long>
    {
        public AbstractTuneUpRecord()
        {
            Created = System.DateTime.Now;
        }

        [Key]
        public long Id { get; set; }

        public System.DateTime Created { get; private set; }

        public User Agent { get; set; }

        public float Amount { get; set; }
        
    }
}

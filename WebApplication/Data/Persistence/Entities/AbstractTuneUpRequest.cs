using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractTuneUpRequest : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [Key]
        public System.Guid Id { get; set; }

        public System.DateTime Requested { get; set; }

        public User Agent { get; set; }
    }
}

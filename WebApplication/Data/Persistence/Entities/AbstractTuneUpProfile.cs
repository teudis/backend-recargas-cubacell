using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractTuneUpProfile : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IHabilitableEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public bool Enabled { get; set; }

        [Required]
        public float Amount { get; set; }

        public string Label { get; set; }

        public bool Primary { get; set; }
    }
}

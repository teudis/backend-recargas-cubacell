using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    public class CellularBalanceTuneUpRecord : AbstractTuneUpRecord
    {
        [Required, Phone]
        public string PhoneNumberTarget { get; set; }
    }
}

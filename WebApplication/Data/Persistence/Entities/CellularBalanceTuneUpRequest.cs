using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    public class CellularBalanceTuneUpRequest : AbstractTuneUpRequest
    {
        [Required, Phone]
        public string PhoneNumberTarget { get; set; }

        public CellularBalanceTuneUpProfile TuneUpProfile { get; set; }
    }
}

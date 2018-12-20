using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    public class NautaBalanceTuneUpRecord : AbstractTuneUpRecord
    {
        [Required, EmailAddress]
        public string EmailAddressTarget { get; set; }
    }
}

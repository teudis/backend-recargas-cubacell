using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    public class NautaBalanceTuneUpRequest : AbstractTuneUpRequest
    {
        [Required, EmailAddress]
        public string EmailAddressTarget { get; set; }

        public NautaBalanceTuneUpProfile TuneUpProfile { get; set; }
    }
}

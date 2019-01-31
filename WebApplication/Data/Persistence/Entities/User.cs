using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser, SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<string>
    {
        [Required]
        public string FullName { get; set; }
        public string IdRole { get; set; }
        public Account Account { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    public class Account : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IHabilitableEntity<System.Guid>
    {
        public Account()
        {
            Members = new System.Collections.Generic.List<IdentityUser>();
        }

        [Key]
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        [Required]
        public string Name { get; set; }

        public float Balance { get; set; }

        public IdentityUser Representative { get; set; }

        public System.Collections.Generic.ICollection<IdentityUser> Members { get; set; }
    }
}

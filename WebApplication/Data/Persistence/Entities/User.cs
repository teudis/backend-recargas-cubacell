namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public Account Account { get; set; }
    }
}

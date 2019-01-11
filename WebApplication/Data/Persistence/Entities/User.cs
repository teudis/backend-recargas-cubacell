namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser, SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<string>
    {
        public string FullName { get; set; }        
        public Account Account { get; set; }
    }
}
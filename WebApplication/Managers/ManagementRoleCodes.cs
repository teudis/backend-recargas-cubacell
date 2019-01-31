using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers
{
    public class ManagementRoleCodes
    {
        public const string ADMINISTRADOR = "Administrator";
        public const string MANAGER = "Manager";
        public const string MEMBER = "Member";
        public const string MANAGER_MEMBER = "Manager_Member";

        public List<string> GetRoles ()
        {
            var lista = new List<string>() {ADMINISTRADOR,MANAGER,MEMBER};
            return lista;
        }
        
    }  
    
}

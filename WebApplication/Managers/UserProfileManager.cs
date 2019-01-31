using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers
{
    public class UserProfileManager : IUserManager
    {
        private UserManager<User> _userManager;
        private IPasswordHasher<User> _identityPasswordHasher = new PasswordHasher<User>();
        private RoleManager<IdentityRole> roleManager;

        public UserProfileManager(UserManager<User> _userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = _userManager;
            this.roleManager = roleManager;
            
        }

        public void AddRoleDefault(User User)
        {
             var rol = roleManager.FindByIdAsync(User.IdRole).GetAwaiter().GetResult().Name;            
            _userManager.AddToRoleAsync(User, rol).GetAwaiter().GetResult();          

        }
      

        public  void GenerateSecurtyStampAsync(User User)
        {
             _userManager.UpdateSecurityStampAsync(User).GetAwaiter().GetResult();  
        }

        public List<IdentityRole> GetRoles()
        {
           return roleManager.Roles.ToList();          
           
        }

        public string HashPassword(User user, string password)
        {
            return _identityPasswordHasher.HashPassword(user, password);
        }

        public void UpdateRole(User user)
        {

            string roldata = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            var rolnew = roleManager.FindByIdAsync(user.IdRole).GetAwaiter().GetResult();
            if (roldata != rolnew.Name)
            {
                _userManager.RemoveFromRoleAsync(user, roldata).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, rolnew.Name).GetAwaiter().GetResult();
            }
        }
    }


  public  interface IUserManager
    {
        void GenerateSecurtyStampAsync(User User);
        void AddRoleDefault(User User);
        string HashPassword(User user, string password);
        List<IdentityRole> GetRoles();
        void UpdateRole(User user);

    }

}

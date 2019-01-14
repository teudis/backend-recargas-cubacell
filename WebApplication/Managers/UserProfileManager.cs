﻿using Microsoft.AspNetCore.Identity;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers
{
    public class UserProfileManager : IUserManager
    {
        private UserManager<User> _userManager;

        public UserProfileManager(UserManager<User> _userManager)
        {
            this._userManager = _userManager;

        }

        public void AddRoleDefault(User User)
        {
             _userManager.AddToRoleAsync(User, "Manager").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(User, "Member").GetAwaiter().GetResult();

        }
      

        public  void GenerateSecurtyStampAsync(User User)
        {
             _userManager.UpdateSecurityStampAsync(User).GetAwaiter().GetResult();  
        }

       
    }



  public  interface IUserManager
    {
        void GenerateSecurtyStampAsync(User User);
        void AddRoleDefault(User User);
        

    }

}

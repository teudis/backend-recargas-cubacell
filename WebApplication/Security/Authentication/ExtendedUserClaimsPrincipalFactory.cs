using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authentication
{
    public class ExtendedUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public ExtendedUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity =  await base.GenerateClaimsAsync(user);

            var account = UserManager.Users.Include(entity => entity.Account).First(entity => entity.Id == user.Id).Account;

            if (account != null)
            {
                identity.AddClaim(new Claim(Authorization.Claims.ACCOUNT_CLAIM, user.Account.Id.ToString()));
            }

            return identity;
        }
    }
}

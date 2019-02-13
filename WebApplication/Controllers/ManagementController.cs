using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers
{
    [Authorize(Roles = Security.Authorization.Roles.SYSTEM_ADMIN_ROLE)]
    public class ManagementController : BaseWebController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserManager profilemanager;
        public ManagementController(UserManager<User> _userManager, IUserManager profilemanager, IStringLocalizer<ManagementController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
        {
            this._userManager = _userManager;
            this.profilemanager = profilemanager;
        }

        public IActionResult Index()
        {
            return View();
        }

        
         public IActionResult Profile()
        {
            var current_user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            ViewBag.FullName = current_user.FullName;
            ViewBag.Email = current_user.Email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile([Bind("Password,ConfirmPassword")] ChangePasswordModelView modelinput)
        {
            if (ModelState.IsValid)
            {
                var current_user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
                current_user.PasswordHash = profilemanager.HashPassword(current_user, modelinput.Password);
                await _userManager.UpdateAsync(current_user);
                return RedirectToAction(nameof(Index));
            }          

            return View();
        }
    }
}
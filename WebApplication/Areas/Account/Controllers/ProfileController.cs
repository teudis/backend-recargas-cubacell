using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Areas.Account.Controllers
{
    [Area("Account")]
    [Authorize]
    public class ProfileController : BaseWebController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public ProfileController(ApplicationDbContext _context, UserManager<User> _userManager, IStringLocalizer<ProfileController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
        {
            this._context = _context;
            this._userManager = _userManager;
        }
        public IActionResult Index()
        {
            var current_user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            var modelinput = new UserInputViewModel();
            modelinput.FullName = current_user.FullName;
            modelinput.Email = current_user.Email;
            modelinput.PhoneNumber = current_user.PhoneNumber;
            modelinput.Id = current_user.Id;
            return View(modelinput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FullName,Email,PhoneNumber")] UserInputViewModel modelinput)
        {
            if (id != modelinput.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.FullName = modelinput.FullName;
                    user.Email = modelinput.Email;
                    user.PhoneNumber = modelinput.PhoneNumber;                    
                    await _userManager.UpdateAsync(user);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(modelinput.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(modelinput);
        }

        private bool UserExists(string id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
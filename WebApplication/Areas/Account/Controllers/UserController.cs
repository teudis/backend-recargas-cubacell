using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Helpers.Pagination;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authorization;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Areas.Account.Controllers
{
    [Area("Account")]
    [Authorize(Roles = Roles.ACCOUNT_ADMIN_ROLE, Policy =Policies.ACCOUNT_ASSOCIATED)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUserManager profilemanager;

        public UserController(ApplicationDbContext context, UserManager<User> _userManager, IUserManager profilemanager)
        {
            _context = context;
            this._userManager = _userManager;
            this.profilemanager = profilemanager;
        }

        // GET: Usuarios
        public async Task<ActionResult> Index(int? pageNumber)
        {
           var user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
           var accountid = HttpContext.User.FindFirst(Claims.ACCOUNT_CLAIM).Value;
           var data = _context.Usuarios.Where(account => account.Account.Id == Guid.Parse(accountid)).Where(usuario => usuario.Id != user.Id);
            //return View(await data.ToListAsync());
           int pageSize = 20;
           return View(await PaginatedList<User>.CreateAsync(data.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            var entities = profilemanager.GetRoles();            

            var selectListItems = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                if (entity.Name != Roles.SYSTEM_ADMIN_ROLE)
                {
                    selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
                }

            }

            var modelinput = new UserWithPasswordInputViewModel();
            modelinput.Roles = selectListItems;

            return View(modelinput);
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserWithPasswordInputViewModel modelinput)
        {           

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                
                var user = new User {

                FullName = modelinput.FullName,
                UserName = modelinput.Email,
                Email = modelinput.Email,
                NormalizedEmail = modelinput.Email.ToUpper(),
                NormalizedUserName = modelinput.Email.ToUpper(),                
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                PhoneNumber = modelinput.PhoneNumber,
                IdRole = modelinput.IdRole

                };

                 user.PasswordHash = profilemanager.HashPassword(user, modelinput.Password);                
                 var user_current_id = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult().Id;
                 var account_user = _context.Accounts.Where(idRepresentative => idRepresentative.RepresentativeId == user_current_id).ToList()[0];                 
                 user.Account = (SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities.Account)account_user;
                 _context.Add(user);               

                await _context.SaveChangesAsync();
                await _userManager.AddToRoleAsync(user, Roles.ACCOUNT_SELLER_ROLE);

                return RedirectToAction(nameof(Index));

                 }

                return View(User);        
           
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var entities = profilemanager.GetRoles();

            var selectListItems = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                if (entity.Name != Roles.SYSTEM_ADMIN_ROLE)
                {
                    selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
                }

            }

            var modelinput = new UserWithPasswordInputViewModel();
            modelinput.Roles = selectListItems;

            return View(new UserInputViewModel {

                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Id =  user.Id,
                Roles = modelinput.Roles,
                IdRole = user.IdRole
            });
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [Bind("Id,FullName,Email,PhoneNumber,IdRole")] UserInputViewModel modelinput)
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
                    user.IdRole = modelinput.IdRole;
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
       

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
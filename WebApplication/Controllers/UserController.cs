using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers
{    
    [Authorize(Roles = Security.Authorization.Roles.ACCOUNT_ADMIN_ROLE, Policy = Security.Authorization.Policies.ACCOUNT_ASSOCIATED)]
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
        public async Task<ActionResult> Index()
        {
           var user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
           var idUser = _context.Usuarios.Where(id => id.Id == user.Id).ToList()[0].Id;
           var account_user = _context.Accounts.Where(idRepresentative => idRepresentative.RepresentativeId == idUser).ToList()[0].Id;
           var data = _context.Usuarios.Where(users => users.Account.Id == account_user);

           return View(await data.ToListAsync());
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
            return View();
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
                PhoneNumber = modelinput.PhoneNumber
                };

                 user.PasswordHash = profilemanager.HashPassword(user, modelinput.Password);                
                 var user_current_id = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult().Id;
                 var account_user = _context.Accounts.Where(idRepresentative => idRepresentative.RepresentativeId == user_current_id).ToList()[0];                 
                 user.Account = (Account)account_user;
                 _context.Add(user);               

                await _context.SaveChangesAsync();
                await _userManager.AddToRoleAsync(user, Security.Authorization.Roles.ACCOUNT_SELLER_ROLE);

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

            return View(new UserInputViewModel {

                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Id =  user.Id
            });
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [Bind("Id,FullName,Email,PhoneNumber")] UserInputViewModel modelinput)
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
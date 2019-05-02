using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.ViewComponents
{
    public class AccountViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AccountViewComponent(ApplicationDbContext _context, UserManager<User> _userManager)
        {
            this._context = _context;
            this._userManager = _userManager;
        }

        public IViewComponentResult Invoke()
        {
            var accountid = HttpContext.User.FindFirst(Claims.ACCOUNT_CLAIM).Value;
            var monto = _context.Accounts.Where(account => account.Id == Guid.Parse(accountid)).Select(c=>c.Balance).SingleOrDefault();
            ViewData["saldo"] = monto;
            return View();
        }
    }
}

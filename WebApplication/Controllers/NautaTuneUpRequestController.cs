using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Helpers.Pagination;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers
{
    [Area("Account")]
    [Authorize]
    public class NautaTuneUpRequestController : BaseWebController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public NautaTuneUpRequestController(ApplicationDbContext _context, UserManager<User> _userManager, IStringLocalizer<NautaTuneUpRequestController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
        {
            this._context = _context;
            this._userManager = _userManager;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var data = _context.NautaBalanceTuneUpRequests.Include(tuneuprofile => tuneuprofile.TuneUpProfile).Where(user => user.Agent.Id == current_user.Id);
            //return View(await data.ToListAsync());
            int pageSize = 20;
            return View(await PaginatedList<NautaBalanceTuneUpRequest>.CreateAsync(data.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<ActionResult> Details(System.Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nautatuneuprequest = await _context.NautaBalanceTuneUpRequests.FindAsync(id);
            if (nautatuneuprequest == null)
            {
                return NotFound();
            }

            return View(nautatuneuprequest);
        }

    }
}
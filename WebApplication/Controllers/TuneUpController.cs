using System;
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
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers
{
    [Area("Account")]
    [Authorize]
    public class TuneUpController : BaseWebController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;        

        public TuneUpController(ApplicationDbContext _context, UserManager<User> _userManager, IStringLocalizer<TuneUpController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
        {
            this._context = _context;
            this._userManager = _userManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPerfilNauta()
        {
            var result = _context.NautaBalanceTuneUpProfiles.ToList();
            return Json(result);
        }

        public IActionResult GetPerfilCubacel()
        {
            var result = _context.CellularBalanceTuneUpProfiles.ToList();
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCelullarBalanceTuneUpRequest([FromBody]  RecargaCubacelModelView [] model)
        {
            var current_user =  await _userManager.GetUserAsync(HttpContext.User);            
            foreach (var cubacel in model)
            {
                var CellTuneUpProfile = await _context.CellularBalanceTuneUpProfiles.FindAsync(cubacel.Id);
                CellularBalanceTuneUpRequest cell = new CellularBalanceTuneUpRequest
                {
                    Requested = DateTime.Now,
                    Agent = current_user,
                    PhoneNumberTarget = cubacel.PhoneNumberTarget,
                    TuneUpProfile = CellTuneUpProfile
                };
                _context.Add(cell);
            }
            await _context.SaveChangesAsync();
            return Json(new { someValue = "Ok" });
        }

        [HttpPost]
        public async Task<IActionResult> InsertNautaBalanceTuneUpRequest([FromBody]  RecargaNautaModelView[] model)
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            foreach (var nauta in model)
            {
                var CellTuneUpProfile = await _context.NautaBalanceTuneUpProfiles.FindAsync(nauta.Id);
                NautaBalanceTuneUpRequest hogar = new NautaBalanceTuneUpRequest
                {
                    Requested = DateTime.Now,
                    Agent = current_user,
                    EmailAddressTarget = nauta.EmailAddressTarget,
                    TuneUpProfile = CellTuneUpProfile
                };
                _context.Add(hogar);
            }
            await _context.SaveChangesAsync();
            return Json(new { someValue = "Recargas Procesadas"});

        }

    }
}
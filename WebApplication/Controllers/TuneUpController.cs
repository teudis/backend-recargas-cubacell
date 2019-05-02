using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
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

        public async Task<IActionResult> Nauta()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var last_cubacel = _context.NautaBalanceTuneUpRequests.Where(u => u.Agent.Id == current_user.Id).Select(d => new NautaBalanceTuneUpRequest { EmailAddressTarget = d.EmailAddressTarget }).Distinct().Take(10).ToList();
            return View(last_cubacel);
        }

        public async Task<IActionResult> Cubacel()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var last_celular = _context.CellularBalanceTuneUpRequests.Where(u=>u.Agent.Id == current_user.Id).Select(d => new CellularBalanceTuneUpRequest {PhoneNumberTarget = d.PhoneNumberTarget }).Distinct().Take(10).ToList();           
            return View(last_celular);
        }

        [HttpGet]
        public JsonResult NautaTuneUpProfiles()
        {
            var result = _context.NautaBalanceTuneUpProfiles.ToList();

            return Json(result);
        }

        public IActionResult CubacelTuneUpProfiles()
        {
            var result = _context.CellularBalanceTuneUpProfiles.ToList();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> RequestCubacelTuneUp([FromBody]  RecargaCubacelModelView [] model)
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

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Success,
                Message = "Recargas solicitadas con éxito."
            };

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return Json(new { result = "OK" });
        }

        [HttpPost]
        public async Task<IActionResult> RequestNautaTuneUp([FromBody]  RecargaNautaModelView[] model)
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

            var notification = new NotificationViewModel
            {
                Type = NotificationType.Success,
                Message = "Recargas solicitadas con éxito."
            };

            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);

            return Json(new { result = "OK" });
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers;

namespace WebApplication.Controllers
{
    [Authorize(Roles = ManagementRoleCodes.ADMINISTRADOR)]
    public class ManagementController : BaseWebController
    {
        public ManagementController(IStringLocalizer<ManagementController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers
{
    [Authorize(Roles = Security.Authorization.Roles.SYSTEM_ADMIN_ROLE)]
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
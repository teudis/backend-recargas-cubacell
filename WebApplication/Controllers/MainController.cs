using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers;

namespace WebApplication.Controllers
{
    [Authorize(Roles = ManagementRoleCodes.ADMINISTRADOR)]
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
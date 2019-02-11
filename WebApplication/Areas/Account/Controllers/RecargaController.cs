using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;

namespace WebApplication.Areas.Account.Controllers
{
    [Area("Account")]
    public class RecargaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecargaController(ApplicationDbContext _context)
        {
            this._context = _context;

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

    }
}
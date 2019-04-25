using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Helpers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authorization;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers
{
    [Area("Account")]
    [Authorize]
    public class SearchController : BaseWebController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public SearchController(ApplicationDbContext _context, UserManager<User> _userManager, IStringLocalizer<SearchController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
        {
            this._context = _context;
            this._userManager = _userManager;

        }

        [Authorize(Roles = Roles.ACCOUNT_SELLER_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        public IActionResult SearchBalanceTune()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SearchBalanceTuneResult(SearchInputModelView modelinput)
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);

            if (modelinput.PerfilRecarga == Perfil.PERFIL_NAUTA)
            {

                modelinput.UserId = current_user.Id;
                var listado_nauta = GetNautaRecord(modelinput);
                return View("SearchBalanceTuneResultNauta", listado_nauta);
            }
            else
            {
                modelinput.UserId = current_user.Id;
                var listado_cubacel = GetCelularRecord(modelinput);
                return View("SearchBalanceTuneResultCubacel", listado_cubacel);
            }

        }

        [Authorize(Roles = Roles.ACCOUNT_SELLER_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        private List<NautaBalanceTuneUpRecord> GetNautaRecord(SearchInputModelView modelinput)
        {
            var fecha_ini = modelinput.FechaInicio;
            var fecha_fin = modelinput.FechaFin;
            var email = modelinput.EmailTarget;
            var userid = modelinput.UserId;
            var listado_nauta = new List<NautaBalanceTuneUpRecord>();
            // caso 1 perfilnauta- rango de fecha - sin email nauta 
            if (!fecha_ini.Equals(new DateTime(1, 1, 1)) && !fecha_fin.Equals(new DateTime(1, 1, 1)) && email == null)
            {
                listado_nauta = _context.NautaBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where
                (recordnauta => recordnauta.Agent.Id == userid && (recordnauta.Created >= fecha_ini && recordnauta.Created <= fecha_fin)).ToList();

            }
            else  // caso 2 perfilnauta- rango de fecha -  email nauta 
                if (!fecha_ini.Equals(new DateTime(1, 1, 1)) && !fecha_fin.Equals(new DateTime(1, 1, 1)) && email != null)
            {
                listado_nauta = _context.NautaBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where
                (recordnauta => recordnauta.Agent.Id == userid && recordnauta.EmailAddressTarget == email && (recordnauta.Created >= fecha_ini && recordnauta.Created <= fecha_fin)).ToList();

            }
            else // caso 3 perfilnauta- sin rango de fecha -  email nauta 
                if (fecha_ini.Equals(new DateTime(1, 1, 1)) && fecha_fin.Equals(new DateTime(1, 1, 1)) && email != null)
            {

                listado_nauta = _context.NautaBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where
                (recordnauta => recordnauta.Agent.Id == userid && recordnauta.EmailAddressTarget == email).ToList();

            }
            else // caso 4 solo perfilnauta- sin rango de fecha - sin email nauta 
            {

                listado_nauta = _context.NautaBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where(user => user.Agent.Id == userid).ToList();
            }

            return listado_nauta;
        }


        private List<CellularBalanceTuneUpRecord> GetCelularRecord(SearchInputModelView modelinput)
        {
            var fecha_ini = modelinput.FechaInicio;
            var fecha_fin = modelinput.FechaFin;
            var phone = modelinput.PhoneTarget;
            var userid = modelinput.UserId;
            var listado_cubacel = new List<CellularBalanceTuneUpRecord>();
            // caso 1 perfilcubacel- rango de fecha - sin phone
            if (!fecha_ini.Equals(new DateTime(1, 1, 1)) && !fecha_fin.Equals(new DateTime(1, 1, 1)) && phone == null)
            {
                listado_cubacel = _context.CellularBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where
                (recordcubacel => recordcubacel.Agent.Id == userid && (recordcubacel.Created >= fecha_ini && recordcubacel.Created <= fecha_fin)).ToList();

            }
            else  // caso 2 perfilcubacel- rango de fecha -  phone
                if (!fecha_ini.Equals(new DateTime(1, 1, 1)) && !fecha_fin.Equals(new DateTime(1, 1, 1)) && phone != null)
            {
                listado_cubacel = _context.CellularBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where
                (recordcubacel => recordcubacel.Agent.Id == userid && recordcubacel.PhoneNumberTarget == phone && (recordcubacel.Created >= fecha_ini && recordcubacel.Created <= fecha_fin)).ToList();

            }
            else // caso 3 perfilcubacel- sin rango de fecha -  phone
                if (fecha_ini.Equals(new DateTime(1, 1, 1)) && fecha_fin.Equals(new DateTime(1, 1, 1)) && phone != null)
            {

                listado_cubacel = _context.CellularBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where
                (recordnauta => recordnauta.Agent.Id == userid && recordnauta.PhoneNumberTarget == phone).ToList();

            }
            else // caso 4 solo perfilcubacel- sin rango de fecha - sin phone
            {
                listado_cubacel = _context.CellularBalanceTuneUpRecords.Where(user => user.Agent.Id == userid).ToList();
            }

            return listado_cubacel;
        }

        [Authorize(Roles = Roles.ACCOUNT_ADMIN_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        public IActionResult SearchBalanceTuneAccount()
        {
            var accountid = HttpContext.User.FindFirst(Claims.ACCOUNT_CLAIM).Value;
            var listado_usuarios = _context.Usuarios.Where(user => user.Account.Id == Guid.Parse(accountid)).ToList();
            var selectListItems = new List<SelectListItem>();
            foreach (var user in listado_usuarios)
            {
                selectListItems.Add(new SelectListItem { Value = user.Id.ToString(), Text = user.FullName });
            }

            ViewData["usuarios"] = selectListItems;
            return View();
        }

        [HttpPost]
        public IActionResult SearchBalanceTuneAccountResult(SearchInputModelView modelinput)
        {
            if (modelinput.PerfilRecarga == Perfil.PERFIL_NAUTA)
            {


                var listado_nauta = GetNautaRecord(modelinput);
                var accountid = HttpContext.User.FindFirst(Claims.ACCOUNT_CLAIM).Value;
                var listado_usuarios = _context.Usuarios.Where(user => user.Account.Id == Guid.Parse(accountid)).ToList();
                var selectListItems = new List<SelectListItem>();
                foreach (var user in listado_usuarios)
                {
                    selectListItems.Add(new SelectListItem { Value = user.Id.ToString(), Text = user.FullName });
                }

                ViewData["usuarios"] = selectListItems;
                return View("SearchBalanceTuneResultNautaAccount", listado_nauta);
            }
            else
            {
                var listado_cubacel = GetCelularRecord(modelinput);
                var accountid = HttpContext.User.FindFirst(Claims.ACCOUNT_CLAIM).Value;
                var listado_usuarios = _context.Usuarios.Where(user => user.Account.Id == Guid.Parse(accountid)).ToList();
                var selectListItems = new List<SelectListItem>();
                foreach (var user in listado_usuarios)
                {
                    selectListItems.Add(new SelectListItem { Value = user.Id.ToString(), Text = user.FullName });
                }

                ViewData["usuarios"] = selectListItems;
                return View("SearchBalanceTuneResultCubacelAccount", listado_cubacel);
            }

        }

    }
}
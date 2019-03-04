﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authorization;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Areas.Account.Controllers
{
    [Area("Account")]
    [Authorize]
    public class RecargaController : BaseWebController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;        

        public RecargaController(ApplicationDbContext _context, UserManager<User> _userManager, IStringLocalizer<RecargaController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
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

        [Authorize(Roles = Roles.ACCOUNT_SELLER_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        public async Task<IActionResult> GetCellularBalanceTuneRecord()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);

            var listado = _context.CellularBalanceTuneUpRecords.Where(record => record.Agent.Id == current_user.Id).ToList();
            return View(listado);
        }

        [Authorize(Roles = Roles.ACCOUNT_SELLER_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        public async Task<IActionResult> GetNautaBalanceTuneRecord()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var listado = _context.NautaBalanceTuneUpRecords.Where(record => record.Agent.Id == current_user.Id).ToList();
            return View(listado);
        }

        [Authorize(Roles = Roles.ACCOUNT_ADMIN_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        public async Task<IActionResult> GetNautaBalanceTuneRecordAccount()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var accountid = HttpContext.User.FindFirst(Claims.ACCOUNT_CLAIM).Value;
            var result = _context.NautaBalanceTuneUpRecords.Include(user => user.Agent).Where(r => r.Agent.Account.Id == Guid.Parse(accountid)).ToList();
            return View(result);
        }

        [Authorize(Roles = Roles.ACCOUNT_ADMIN_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        public async Task<IActionResult> GetCellularBalanceTuneRecordAccount()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var accountid = HttpContext.User.FindFirst(Claims.ACCOUNT_CLAIM).Value;
            var result = _context.CellularBalanceTuneUpRecords.Include(user => user.Agent).Where(r => r.Agent.Account.Id == Guid.Parse(accountid)).ToList();
            return View(result);
        }

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
                    return View("SearchBalanceTuneResultNauta",listado_nauta);
                }
                else
                {
                    modelinput.UserId = current_user.Id;
                    var listado_cubacel = GetCelularRecord(modelinput);
                    return View("SearchBalanceTuneResultCubacel", listado_cubacel);                    
                }
            
        }


        private List <NautaBalanceTuneUpRecord> GetNautaRecord(SearchInputModelView modelinput)
        {
            var fecha_ini = modelinput.FechaInicio;
            var fecha_fin = modelinput.FechaFin;
            var email = modelinput.EmailTarget;
            var userid = modelinput.UserId;
            var listado_nauta = new List<NautaBalanceTuneUpRecord>();
            // caso 1 perfilnauta- rango de fecha - sin email nauta 
            if (!fecha_ini.Equals(new DateTime(1,1,1)) && !fecha_fin.Equals(new DateTime(1, 1, 1)) && email == null)
            {
                listado_nauta = _context.NautaBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where
                (recordnauta => recordnauta.Agent.Id == userid && (recordnauta.Created >= fecha_ini && recordnauta.Created <= fecha_fin)).ToList();

            }
            else  // caso 2 perfilnauta- rango de fecha -  email nauta 
                if (!fecha_ini.Equals(new DateTime(1, 1, 1)) && !fecha_fin.Equals(new DateTime(1, 1, 1)) && email != null )
            {
                listado_nauta = _context.NautaBalanceTuneUpRecords.Include(usuario => usuario.Agent).Where
                (recordnauta => recordnauta.Agent.Id == userid && recordnauta.EmailAddressTarget == email && (recordnauta.Created >= fecha_ini && recordnauta.Created <= fecha_fin)).ToList();

            }
            else // caso 3 perfilnauta- sin rango de fecha -  email nauta 
                if (fecha_ini.Equals(new DateTime(1, 1, 1)) && fecha_fin.Equals(new DateTime(1, 1, 1)) && email != null )
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
                listado_cubacel = _context.CellularBalanceTuneUpRecords.Where(user =>user.Agent.Id == userid).ToList();
            }

            return listado_cubacel;
        }


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
            if (modelinput.PerfilRecarga == Perfil.PERFIL_NAUTA)            {

                
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
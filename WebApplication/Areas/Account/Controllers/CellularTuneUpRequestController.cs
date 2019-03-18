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

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Areas.Account.Controllers
{
    [Area("Account")]
    [Authorize]
    public class CellularTuneUpRequestController : BaseWebController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public CellularTuneUpRequestController(ApplicationDbContext _context, UserManager<User> _userManager, IStringLocalizer<CellularTuneUpRequestController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
        {
            this._context = _context;
            this._userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var data = _context.CellularBalanceTuneUpRequests.Include(tuneuprofile => tuneuprofile.TuneUpProfile).Where(user => user.Agent.Id == current_user.Id);
            return View(await data.ToListAsync());
        }


        public async Task<ActionResult> Details(System.Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var celulartuneuprequest = await _context.CellularBalanceTuneUpRequests.FindAsync(id);
            if (celulartuneuprequest == null)
            {
                return NotFound();
            }

            return View(celulartuneuprequest);
        }
    }
}
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
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authorization;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Areas.Account.Controllers
{
    [Area("Account")]
    [Authorize]
    public class CellularTuneUpRecordController : BaseWebController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public CellularTuneUpRecordController(ApplicationDbContext _context, UserManager<User> _userManager, IStringLocalizer<CellularTuneUpRecordController> localizer, ILogger<BaseWebController> logger) : base(localizer, logger)
        {
            this._context = _context;
            this._userManager = _userManager;
        }

        [Authorize(Roles = Roles.ACCOUNT_SELLER_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        public async Task<IActionResult> GetCellularBalanceTuneRecord()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);

            var listado = _context.CellularBalanceTuneUpRecords.Where(record => record.Agent.Id == current_user.Id).ToList();
            return View(listado);
        }

        [Authorize(Roles = Roles.ACCOUNT_ADMIN_ROLE, Policy = Policies.ACCOUNT_ASSOCIATED)]
        public async Task<IActionResult> GetCellularBalanceTuneRecordAccount()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var accountid = HttpContext.User.FindFirst(Claims.ACCOUNT_CLAIM).Value;
            var result = _context.CellularBalanceTuneUpRecords.Include(user => user.Agent).Where(r => r.Agent.Account.Id == Guid.Parse(accountid)).ToList();
            return View(result);
        }


    }
}
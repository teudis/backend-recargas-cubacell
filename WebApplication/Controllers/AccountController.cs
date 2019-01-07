using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;

namespace WebApplication.Controllers
{
    [Area("security")]
    public class AccountController : AbstractEntityManagementController<Account, System.Guid, AccountInputViewModel,AccountDisplayViewModel>
    {
        public AccountController(IEntityRepository<Account, System.Guid> repository, IStringLocalizer<AccountController> localizer, ILogger<AccountController> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }
    }
}
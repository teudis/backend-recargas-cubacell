using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authorization;

namespace WebApplication.Controllers.Mananagement
{
    [Area("Main")]
    [Authorize(Roles = Roles.SYSTEM_ADMIN_ROLE)]
    public class NautaBalanceTuneUpRecordController : AbstractEntityManagementController<NautaBalanceTuneUpRecord, long, NautaBalanceTuneUpRecordInputViewModel, NautaBalanceTuneUpRecordDisplayViewModel>
    {
        public NautaBalanceTuneUpRecordController(IEntityRepository<NautaBalanceTuneUpRecord, long> repository, IStringLocalizer<NautaBalanceTuneUpRecordController> localizer, ILogger<NautaBalanceTuneUpRecordController> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(new NotificationViewModel
            {
                Type = NotificationType.Warning,
                Title = "ActionNoFound",
                Message = "ActionNoFound"
            });
            return RedirectToAction(nameof(Index));
        }

        public override IActionResult Edit(long key)
        {
            TempData[NotificationViewModel.NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(new NotificationViewModel
            {
                Type = NotificationType.Warning,
                Title = "ActionNoFound",
                Message = "ActionNoFound"
            });
            return RedirectToAction(nameof(Index));
        }

        public override IActionResult Delete(long key)
        {
            return base.Delete(key);
        }
    }
}
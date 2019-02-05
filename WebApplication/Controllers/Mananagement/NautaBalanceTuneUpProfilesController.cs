using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers.Mananagement
{
    [Area("Main")]
    [Authorize(Roles = Security.Authorization.Roles.SYSTEM_ADMIN_ROLE)]
    public class NautaBalanceTuneUpProfilesController : SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Controllers.AbstractEntityManagementController<NautaBalanceTuneUpProfile, int, NautaBalanceTuneUpProfileInputViewModel, NautaBalanceTuneUpProfileDisplayViewModel>
    {

        public NautaBalanceTuneUpProfilesController(IEntityRepository<NautaBalanceTuneUpProfile, int> repository, IStringLocalizer<NautaBalanceTuneUpProfilesController> localizer, ILogger<NautaBalanceTuneUpProfilesController> logger) : base(repository, localizer, logger)
        {
        }
       
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

       
        public override IActionResult Create()
        {
            return base.Create();
        }

       
        public override IActionResult Create(NautaBalanceTuneUpProfileInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        protected override void PreCreate(NautaBalanceTuneUpProfile entity, NautaBalanceTuneUpProfileInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);
        }
       
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }
       
        public override IActionResult Edit(NautaBalanceTuneUpProfileInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }
        
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authorization;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers.Mananagement
{
    [Area("Main")]
    [Authorize(Roles = Security.Authorization.Roles.SYSTEM_ADMIN_ROLE)]
    public class AccountsController : AbstractEntityManagementController<Account, System.Guid, AccountInputViewModel,AccountDisplayViewModel>
    {
        private readonly IEntityRepository<User, string> usersRepository;
        private readonly IUserManager profilemanager;

        public AccountsController(IUserManager profilemanager,IEntityRepository<User, string> usersRepository,IEntityRepository<Account, System.Guid> repository, IStringLocalizer<AccountsController> localizer, ILogger<AccountsController> logger) : base(repository, localizer, logger)
        {
            this.usersRepository = usersRepository;
            this.profilemanager = profilemanager;
        }

        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return base.Create();
        }

        public override IActionResult Create(AccountInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        protected override void PreCreate(Account entity, AccountInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Representative = usersRepository.Get(modelInput.RepresentativeId);
        }

        protected override void PostCreate(Account entity, AccountInputViewModel modelInput)
        {
            base.PostCreate(entity, modelInput);

            entity.Representative.Account = entity;

            usersRepository.Update(entity.Representative, User.Identity.Name);
        }

        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        public override IActionResult Edit(AccountInputViewModel modelInput)
        {
           return base.Edit(modelInput);
        }

        protected override void PreEdit(Account entity, AccountInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            entity.Representative = usersRepository.Get(modelInput.RepresentativeId);
        }

        protected override void PostEdit(Account entity, AccountInputViewModel modelInput)
        {
            base.PostEdit(entity, modelInput);

            entity.Representative.Account = entity;

            usersRepository.Update(entity.Representative, User.Identity.Name);
        }

        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }
        
        public override IActionResult Delete(AccountDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PostDelete(Account entity)
        {
            base.PostDelete(entity);
        }

        private void PopulateModelInputForAvailableUser(AccountInputViewModel inputModel)
        {
            var rol = profilemanager.GetRoles().Find(idrol => idrol.Name == Roles.ACCOUNT_ADMIN_ROLE);
            var entities = usersRepository.FindBy(user => user.IdRole == rol.Id && user.Account == null );
            var selectListItems = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.FullName });
            }

            inputModel.Users = selectListItems;
        }

        protected override AccountInputViewModel ConfigureCreate(AccountInputViewModel modelInput)
        {
            PopulateModelInputForAvailableUser(modelInput);

            return base.ConfigureCreate(modelInput);
        }

        protected override AccountInputViewModel ConfigurePostFailCreateValidation(AccountInputViewModel modelInput)
        {
            PopulateModelInputForAvailableUser(modelInput);

            return base.ConfigurePostFailCreateValidation(modelInput);
        }

        protected override AccountInputViewModel ConfigureEdit(Account entity, AccountInputViewModel modelInput)
        {
            PopulateModelInputForAvailableUser(modelInput);

            return base.ConfigureEdit(entity, modelInput);
        }

        protected override AccountInputViewModel ConfigurePostFailEditValidation(AccountInputViewModel modelInput)
        {
            PopulateModelInputForAvailableUser(modelInput);

            return base.ConfigurePostFailEditValidation(modelInput);
        }

    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Security.Authorization;
using System.Collections.Generic;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Controllers.Mananagement
{
    [Area("Main")]
    [Authorize(Roles = Security.Authorization.Roles.SYSTEM_ADMIN_ROLE)]
    public class UsersController : AbstractEntityManagementController<User, string, UserInputViewModel, UserDisplayViewModel>
    {

        private readonly IUserManager profilemanager;        

        public UsersController(IUserManager profilemanager, IEntityRepository<User, string> repository, IStringLocalizer<UsersController> localizer, ILogger<UsersController> logger) : base(repository, localizer, logger)
        {
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

        public override IActionResult Create(UserInputViewModel modelInput)
        {
            var passwords = new StringValues();
            var confirmations = new StringValues();

            Request.Form.TryGetValue("Password", out passwords);
            Request.Form.TryGetValue("ConfirmPassword", out confirmations);
           
            modelInput = new UserWithPasswordInputViewModel
            {
                Password = passwords.Count > 0 ? passwords.ToArray()[0] : string.Empty,
                ConfirmPassword = confirmations.Count > 0 ? confirmations.ToArray()[0] : string.Empty,
                Email = modelInput.Email,
                FullName = modelInput.FullName,
                PhoneNumber = modelInput.PhoneNumber,
                Id = modelInput.Id ,
                IdRole = modelInput.IdRole
            };           

            if (TryValidateModel(modelInput))
            {
                var entity = modelInput.Export();
               
                PreCreate(entity, modelInput);

                try
                {
                    Repository.Add(entity, User.Identity.Name);                    
                    PostCreate(entity, modelInput);                   

                    return RedirectToAction(GetDefaultActionName());
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    logger.LogError(e.Message, e.InnerException);

                    ModelState.AddModelError("Id", "UserNameTakedMessage");
                }
            }
           

            return View(modelInput);

        }


        protected override void PostCreate(User entity, UserInputViewModel modelInput)
        {
            
            base.PostCreate(entity, modelInput);           
            //Update DEFAULT ROLE
            profilemanager.AddRoleDefault(entity);
           
        }
        

        protected override void PreCreate(User entity, UserInputViewModel modelInput)
        {
            
            entity.PasswordHash = profilemanager.HashPassword(entity, entity.PasswordHash);
            base.PreCreate(entity, modelInput);
        }

        protected override UserInputViewModel ConfigureCreate(UserInputViewModel modelInput)
        {
            var result = new UserWithPasswordInputViewModel();
            PopulateModelInputForAvailableRoles(modelInput);
            if (modelInput is UserWithPasswordInputViewModel)
            {
                return base.ConfigureCreate(modelInput);
            }
            else
            {
                result.Roles = modelInput.Roles;

            }

            return base.ConfigureCreate(result);

        }

        public override IActionResult Edit(string key)
        {
            return base.Edit(key);
        }

        public override IActionResult Edit(UserInputViewModel modelInput)
        {
            
            
            return base.Edit(modelInput);
        }


        protected override void PreEdit(User entity, UserInputViewModel modelInput)
        {

            base.PreEdit(entity, modelInput);
        }

        protected override void PostEdit(User entity, UserInputViewModel modelInput)
        {
            profilemanager.UpdateRole(entity);
            base.PostEdit(entity, modelInput);

        }

        protected override UserInputViewModel ConfigureEdit(User entity, UserInputViewModel modelInput)
        {
            PopulateModelInputForAvailableRoles(modelInput);
            return base.ConfigureEdit(entity, modelInput);
        }

        protected override UserInputViewModel ConfigurePostFailEditValidation(UserInputViewModel modelInput)
        {
            PopulateModelInputForAvailableRoles(modelInput);
            return base.ConfigurePostFailEditValidation(modelInput);
        }

        public override IActionResult Delete(string key)
        {
            return base.Delete(key);
        }


        public override IActionResult Delete(UserDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PostDelete(User entity)
        {
            base.PostDelete(entity);
        }

        private void PopulateModelInputForAvailableRoles(UserInputViewModel inputModel)
        {
            var entities = profilemanager.GetRoles();

            var selectListItems = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                if(entity.Name != Roles.ACCOUNT_SELLER_ROLE)
                {
                    selectListItems.Add(new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
                }
                
            }

            inputModel.Roles = selectListItems;
        }

        protected override UserInputViewModel ConfigurePostFailCreateValidation(UserInputViewModel modelInput)
        {
            PopulateModelInputForAvailableRoles(modelInput);
            return base.ConfigurePostFailCreateValidation(modelInput);
        }
    }
}
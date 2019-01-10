using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Controllers;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View;

namespace WebApplication.Controllers
{
    [Area("dashboard")]
    public class UserController : AbstractEntityManagementController<User, string, UserInputViewModel, UserDisplayViewModel>
    {

        internal static string HashPassword(string plainPassword)
        {
            var hasher = SHA256.Create();

            var originalBytes = Encoding.ASCII.GetBytes(plainPassword);
            var encodedBytes = hasher.ComputeHash(originalBytes);

            return System.BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }

        public UserController(IEntityRepository<User, string> repository, IStringLocalizer<UserController> localizer, ILogger<UserController> logger) : base(repository, localizer, logger)
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
                Id = modelInput.Id
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


        protected override void PreCreate(User entity, UserInputViewModel modelInput)
        {
            entity.PasswordHash = HashPassword(entity.PasswordHash);
            base.PreCreate(entity, modelInput);
        }

        protected override UserInputViewModel ConfigureCreate(UserInputViewModel modelInput)
        {
            var result = modelInput is UserWithPasswordInputViewModel ? base.ConfigureCreate(modelInput) : new UserWithPasswordInputViewModel();
            return result;
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
            //entity.PasswordHash = HashPassword(modelInput.Password);
            base.PreEdit(entity, modelInput);
        }

        protected override void PostEdit(User entity, UserInputViewModel modelInput)
        {
            base.PostEdit(entity, modelInput);

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
    }
}
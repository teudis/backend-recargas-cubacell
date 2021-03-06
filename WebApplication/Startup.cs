using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Areas.Identity.Services;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Repositories;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Managers;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new System.Version(Configuration.GetSection("MySQLSeverVersion").Value), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);
            }));

            services.AddIdentity<User, IdentityRole>(config => {
                //config.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
          
            var defaultCulture = new System.Globalization.CultureInfo("es");

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(mvc =>
                mvc.ModelBinderProviders.Insert(0, new SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ModelBinding.AbstractsModelBinderProvider())
            )
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(SharedResources));
                }
            )
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
                options.SupportedCultures = new[] { defaultCulture };
                options.SupportedUICultures = new[] { defaultCulture };

                options.RequestCultureProviders.Insert(0, new SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Localization.UrlRequestCultureProvider());
            });

            services.AddSingleton<Helpers.Localization.GlobalViewLocalizationHelper>();          

            services.AddScoped<IEntityRepository<Account, System.Guid>, AccountRepository>();
            services.AddScoped<IEntityRepository<User, string>, UserRepository>();
            services.AddScoped<IEntityRepository<CellularBalanceTuneUpProfile, int>, CellularBalanceTuneUpProfileRepository>();
            services.AddScoped<IEntityRepository<NautaBalanceTuneUpProfile, int>, NautaBalanceTuneUpProfileRepository>();
            services.AddScoped<IEntityRepository<CellularBalanceTuneUpRecord, long>, CellularBalanceTuneUpRecordRepository>();
            services.AddScoped<IEntityRepository<NautaBalanceTuneUpRecord, long>, NautaBalanceTuneUpRecordRepository>();
            services.AddScoped<IUserManager, UserProfileManager>();           

            // Email Services
            services.AddSingleton<IEmailSender, MessageServices>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, Security.Authentication.ExtendedUserClaimsPrincipalFactory>();

            services.AddSingleton<IStringLocalizer<Controllers.Mananagement.AccountsController>, Helpers.Localization.GlobalViewLocalizationHelper>();
            services.AddSingleton<IStringLocalizer<Controllers.Mananagement.CellularBalanceTuneupProfilesController>, Helpers.Localization.GlobalViewLocalizationHelper>();
            services.AddSingleton<IStringLocalizer<Controllers.Mananagement.NautaBalanceTuneUpProfilesController>, Helpers.Localization.GlobalViewLocalizationHelper>();
            services.AddSingleton<IStringLocalizer<Controllers.Mananagement.UsersController>, Helpers.Localization.GlobalViewLocalizationHelper>();

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = System.TimeSpan.FromMinutes(5);
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            })
            .AddDataProtection()
                .SetApplicationName("CubansConnectionTuneupResellCookies")
                .PersistKeysToDbContext<ApplicationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Security.Authorization.Policies.ACCOUNT_ASSOCIATED, 
                    policy => policy.RequireAssertion(
                        context => context.User.HasClaim(
                            match => match.Type == Security.Authorization.Claims.ACCOUNT_CLAIM)));
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider, Microsoft.AspNetCore.Mvc.ViewFeatures.CookieTempDataProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext dbContext, Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions> localizaionOptions, System.IServiceProvider serviceProvider)
        {
            loggerFactory.AddFile("logs/default-{Date}.log", LogLevel.Warning);            

            app.UseRequestLocalization(localizaionOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                dbContext.Database.Migrate();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookiePolicy();

            if (!env.IsDevelopment()) return;

            CreateDefaultUserAndRolesAsync(serviceProvider).Wait();
        }

        private async System.Threading.Tasks.Task CreateDefaultUserAndRolesAsync(System.IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { Security.Authorization.Roles.SYSTEM_ADMIN_ROLE, Security.Authorization.Roles.ACCOUNT_ADMIN_ROLE, Security.Authorization.Roles.ACCOUNT_SELLER_ROLE };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var settingsUserEmail = Configuration.GetSection("UserSettings")["UserEmail"];

            var defaultUser = await UserManager.FindByEmailAsync(settingsUserEmail);

            if (defaultUser == null)
            {
                defaultUser = new User
                {
                    UserName = settingsUserEmail,
                    Email = settingsUserEmail,
                    FullName = "Administrador Principal"
                };

                var createPowerUserTask = await UserManager.CreateAsync(defaultUser, Configuration.GetSection("UserSettings")["UserPassword"]);

                if (createPowerUserTask.Succeeded)
                {
                    await UserManager.AddToRoleAsync(defaultUser, Security.Authorization.Roles.SYSTEM_ADMIN_ROLE);
                }
            }
        }
    }
}

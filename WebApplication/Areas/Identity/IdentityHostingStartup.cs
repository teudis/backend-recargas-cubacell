using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data;

[assembly: HostingStartup(typeof(SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Areas.Identity.IdentityHostingStartup))]
namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
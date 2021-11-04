using CaWorkshop.Application;
using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Infrastructure;
using CaWorkshop.WebUI.Filters;
using CaWorkshop.WebUI.Services;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace CaWorkshop.WebUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructureServices(Configuration);
        services.AddApplicationServices();
        services.AddWebUIServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        //app.UseOpenApi();
        app.UseSwaggerUi3();

        app.UseAuthentication();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}

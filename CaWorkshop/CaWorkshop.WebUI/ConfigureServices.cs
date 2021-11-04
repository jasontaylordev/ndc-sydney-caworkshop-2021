using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.WebUI.Filters;
using CaWorkshop.WebUI.Services;
using NSwag.Generation.Processors.Security;
using NSwag;

namespace CaWorkshop.WebUI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(
        this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddControllersWithViews(options =>
            options.Filters.Add(new ApiExceptionFilterAttribute()));

        services.AddRazorPages();

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "CaWorkshop API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

            configure.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}


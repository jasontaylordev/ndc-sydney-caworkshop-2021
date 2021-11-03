namespace CaWorkshop.WebUI;


public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(
        this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddControllersWithViews();
        services.AddRazorPages();

        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "CaWorkshop API";
        });

        return services;
    }
}


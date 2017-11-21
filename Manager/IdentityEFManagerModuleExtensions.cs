using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Piranha.Manager;


public static class IdentityEFManagerModuleExtensions
{
    public static IServiceCollection AddPiranhaIdentityEFManager(this IServiceCollection services)
    {
        var assembly = typeof(IdentityEFManagerModuleExtensions).GetTypeInfo().Assembly;
        var provider = new EmbeddedFileProvider(assembly, "Piranha.AspNetCore.Identity.EF.Manager");

        // Add the file provider to the Razor view engine
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.FileProviders.Add(provider);
        });

        // Add the manager module
        Piranha.App.Modules.Register<IdentityEFModule> ();

        // Return the service collection
        return services;
    }

}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;


namespace SkeinGang.AdminUI;

/// <summary>
/// Entrypoint for the application part containing the admin UI.
/// </summary>
public static class AdminUIPart
{
    /// <summary>
    /// Add the Admin UI to the application.
    /// </summary>
    /// <param name="services">The MVC builder (from app.AddMvc()).</param>
    /// <returns><paramref name="services"/></returns>
    public static IMvcBuilder AddAdminUI(this IMvcBuilder services)
    {
        services.ConfigureApplicationPartManager(manager =>
        {
            manager.ApplicationParts.Add(new AssemblyPart(typeof(AdminUIPart).Assembly));
        });
        return services;
    }
}
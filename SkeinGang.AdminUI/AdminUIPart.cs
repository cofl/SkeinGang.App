global using SkeinGang.AdminUI.Mappers;

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.FileProviders;
using SkeinGang.AdminUI.Services;

namespace SkeinGang.AdminUI;

/// <summary>
/// Entrypoint for the application part containing the admin UI.
/// </summary>
public static class AdminUIPart
{
    public const string LoginPath = "/admin-ui/login";
    public const string LogoutPath = "/admin-ui/logout";

    /// <summary>
    /// Add the Admin UI to the application.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns><paramref name="builder"/></returns>
    public static WebApplicationBuilder AddAdminUI(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DiscordServerService>();
        builder.Services.AddScoped<PlayerService>();
        builder.Services.AddScoped<TeamService>();
        builder.Services.AddScoped<TeamMemberService>();
        builder.Services.AddSingleton<SelectEnumService>();

        // This is apparently fine to call twice?
        builder.Services.AddMvc().ConfigureApplicationPartManager(manager =>
        {
            manager.ApplicationParts.Add(new AssemblyPart(typeof(AdminUIPart).Assembly));
        });
        return builder;
    }

    /// <summary>
    /// Add the Admin UI's static resources to the application pipeline. 
    /// </summary>
    /// <param name="app">The built application.</param>
    /// <returns><paramref name="app" /></returns>
    public static WebApplication UseAdminUIResources(this WebApplication app)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new ManifestEmbeddedFileProvider(
                assembly: typeof(AdminUIPart).Assembly,
                root: "wwwroot"
            )
        });
        return app;
    }
}
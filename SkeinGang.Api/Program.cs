global using Domain = SkeinGang.Data.Entities;

namespace SkeinGang.Api;

internal static class Program
{
    public static void Main(string[] args)
    {
        // TODO: logging
        var builder = WebApplication.CreateBuilder(args);

        var app = builder
            .ConfigureServices()
            .ConfigurePipeline();

        app.Run();
    }
}
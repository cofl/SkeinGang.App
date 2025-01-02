global using Domain = SkeinGang.Data.Entities;
using SkeinGang.Api;

internal class Program
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

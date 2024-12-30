global using Domain = SkeinGang.Data.Entities;
using SkeinGang.Api;

internal class Program
{
    public static void Main(string[] args)
    {
        // TODO: logging
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder
                .ConfigureServices()
                .ConfigurePipeline();
    
            app.Run();
        }
        catch (Exception ex)
        {
            // TODO: Log
            throw;
        }
        finally
        {
            // TODO: log shutdown
        }
    }
}

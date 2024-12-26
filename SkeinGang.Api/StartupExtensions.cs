using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NodaTime;
using Quartz;
using SkeinGang.Api.Services;
using SkeinGang.Api.Util;
using SkeinGang.Data.Context;

namespace SkeinGang.Api;

internal static class StartupExtensions
{
    internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opts =>
            {
                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                opts.MapType<DateTimeZone>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("America/Los_Angeles"),
                    Description = "An IANA Time Zone ID.",
                });
                opts.MapType<LocalTime>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "HH:mm:ss",
                    Example = new OpenApiString("19:00:00"),
                });
            });
        }
        
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        
        builder.Services.AddMvc().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false));
            options.JsonSerializerOptions.Converters.Add(new DateTimeZoneToStringJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new LocalTimeToStringJsonConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddResponseCaching();
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<DataContext>(name: "db");
        
        builder.Services.AddScoped<TeamService>();
        builder.Services.AddHighlightService();
        
        builder.Services.AddQuartz(options =>
        {
            options.ScheduleHighlightJobs();
        });
        builder.Services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return builder.Build();
    }

    internal static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseReDoc(options =>
            {
                options.NativeScrollbars();
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseResponseCaching();
        
        #pragma warning disable ASP0014
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        #pragma warning restore ASP0014

        return app;
    }
}
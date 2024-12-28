using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NodaTime;
using Quartz;
using SkeinGang.AdminUI;
using SkeinGang.Api.Services;
using SkeinGang.Api.Util;
using SkeinGang.Data.Context;

namespace SkeinGang.Api;

internal static class StartupExtensions
{
    /// <summary>
    /// Register services and configure our application.
    /// </summary>
    /// <param name="builder">A builder for registering services on.</param>
    /// <returns>A built application, with configured services but without a configured pipeline.</returns>
    /// <seealso cref="ConfigurePipeline" />
    /// <remarks>
    /// The order of operations in this method is not significant.
    /// </remarks>
    internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            // Redoc
            builder.Services.AddEndpointsApiExplorer();

            // Swagger
            builder.Services.AddSwaggerGen(opts =>
            {
                opts.DescribeAllParametersInCamelCase();
                opts.SupportNonNullableReferenceTypes();
                opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                opts.SchemaFilter<RequireSomePropertiesSchemaFilter>();
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

        // Register ORM service with the connection from our environment config.
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register middleware for handling requests.
        builder.Services.AddResponseCaching();
        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        builder.Services
            .AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new JsonEnumModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new LocalTimeModelBinderProvider());
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(
                    JsonNamingPolicy.SnakeCaseLower,
                    false));
                options.JsonSerializerOptions.Converters.Add(new DateTimeZoneToStringJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new LocalTimeToStringJsonConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })
            .AddAdminUI();


        // Health Checks
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<DataContext>(name: "db");

        // Our own services.
        builder.Services.AddScoped<TeamService>();
        builder.Services.AddHighlightService();

        // Job Scheduler
        builder.Services.AddQuartz(options => { options.ScheduleHighlightJobs(); });
        builder.Services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });

        return builder.Build();
    }

    /// <summary>
    /// Configure a built web app's response pipeline, before it is started.
    /// </summary>
    /// <param name="app">The app to configure.</param>
    /// <returns>The configured app.</returns>
    /// <remarks>
    /// The order of calls in this method is important. Middleware will be
    /// processed from top to bottom.
    /// </remarks>
    internal static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseCors();
        app.UseResponseCaching();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseReDoc(options => { options.NativeScrollbars(); });
        }

        app.MapControllers();

        return app;
    }
}
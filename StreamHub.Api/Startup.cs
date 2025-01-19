using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StreamHub.Core.Extensions;
using StreamHub.Persistence.Contexts;

namespace StreamHub.Api;

/// <summary>
///     Configures services and the HTTP request pipeline for the application.
/// </summary>
public class Startup
{
    private readonly IConfiguration _configuration;

    /// <summary>
    ///     Initializes the Startup class with the provided configuration.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    ///     Configures services for the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Add Configurations
        services.AddConfigurations(_configuration);

        // Add OpenAPI
        services.AddOpenApi();

        // Add DbContext with pooling and SQLite
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        services.AddDbContextPool<StreamHubDbContext>(options =>
            options.UseSqlite(connectionString));

        // Add repositories
        services.AddRepositories();

        // Add Metadata providers and services
        services.AddMetadataProvidersAndServices();

        // Add API controllers
        services.AddControllers();
    }

    /// <summary>
    ///     Configures the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="env">The web host environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Middlewares
        app.UseHttpsRedirection();
        app.UseRouting();

        // Endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapOpenApi();
            endpoints.MapScalarApiReference();
            endpoints.MapControllers();
        });
    }
}
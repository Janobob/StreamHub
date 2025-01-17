namespace StreamHub.Api;

/// <summary>
///     The main entry point class for the application.
/// </summary>
public static class Program
{
    /// <summary>
    ///     The main entry point class for the application.
    /// </summary>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    ///     Creates and configures the web host builder.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <returns>The configured web host builder.</returns>
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
using Serilog;
using SyriacSources.Backend.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Add more sinks as needed (e.g., file, Seq, etc.)
    .CreateLogger();

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}
app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseOpenApi();

app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));

app.MapEndpoints();

app.UseAuthentication();

app.UseAuthorization();

app.UseRouting();

try
{
    // Run the application
    var task = app.RunAsync();
    await app.InitialiseDatabaseAsync();
    await task;
}
catch (Exception ex)
{
    // Log any startup errors
    Log.Fatal(ex, "Application failed to start.");
}
finally
{
    // Ensure logs are flushed before the application exits
    Log.CloseAndFlush();
}


public partial class Program { }

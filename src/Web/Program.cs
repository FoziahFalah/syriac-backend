using FluentValidation;
using Microsoft.Extensions.FileProviders;
using Serilog;
using SyriacSources.Backend.Application.Sources;
using SyriacSources.Backend.Application.Sources.Commands.CreateSource;
using SyriacSources.Backend.Domain.Common.Interfaces;
using SyriacSources.Backend.Infrastructure.Common.Services;
using SyriacSources.Backend.Infrastructure.Data;
using SyriacSources.Backend.Web.Endpoints;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder);
builder.Services.AddWebServices();
builder.Services.AddAutoMapper(
    typeof(SourceDto.Mapping),
    typeof(AttachmentDto.Mapping),
    typeof(CoverPhotoDto.Mapping),
    typeof(PublicationDto.Mapping),
    typeof(SourceDateDto.Mapping)
);
builder.Services.AddScoped<IFileServices, FileServices>(provider =>
{
    var env = provider.GetRequiredService<IWebHostEnvironment>();
    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
    return new FileServices(env.WebRootPath, accessor);
});

// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Add more sinks as needed (e.g., file, Seq, etc.)
    .CreateLogger();

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);



if (app.Environment.IsDevelopment())
{
    app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}
app.UseCors("AllowFrontend");
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





app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();

app.UseStaticFiles(); 
app.UseStaticFiles(new StaticFileOptions
{
FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
RequestPath = "/uploads"
});






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

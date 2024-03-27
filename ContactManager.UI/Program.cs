using ContactManager.Application.RepoInterface;
using ContactManager.Application.ServicesImplementation;
using ContactManager.Application.ServicesInterface;
using ContactManager.InfraStructure.RepoImplementation;
using ContactManager.UI.MiddleWare;
using ContactManager.UI.StartUpExtension;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Configure Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerconfiguration) =>
{
    loggerconfiguration.ReadFrom.Configuration(context.Configuration)
      .ReadFrom.Services(services);
});


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseExceptionHandlingMiddleware();
}

app.UseSerilogRequestLogging();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

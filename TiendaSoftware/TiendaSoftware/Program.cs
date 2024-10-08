using TiendaSoftware;
using TiendaSoftware.DataBase;



var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.configureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<TiendaSoftwareContext>();
        await TiendaSoftwareSeeder.LoadDataAsync(context, loggerFactory);
    }
    catch (Exception e)
    {
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "Error al ejevutar el seed de datos");

        }
    }
}

app.Run();

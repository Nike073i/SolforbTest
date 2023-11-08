using Serilog;
using SolforbTest.Application;
using SolforbTest.EfContext.SqlServer;
using SolforbTest.WebClient.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddSerilog();

var configuration = builder.Configuration;
string dbConnectionString = DbConfig.GetConnectionString(configuration);
Log.Logger = LogConfig.GetLoggerConfiguration(configuration);

var services = builder.Services;
services.AddSqlServerDb(dbConnectionString);
services.AddApplication();
services.AddMvc();

var app = builder.Build();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");
app.Run();

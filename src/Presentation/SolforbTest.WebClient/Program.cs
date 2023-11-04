using MediatR;
using SolforbTest.Application;
using SolforbTest.Application.Providers.Queries.GetProviderList;
using SolforbTest.EfContext.SqlServer;
using SolforbTest.WebClient.Config;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
string dbConnectionString = DbConfig.GetConnectionString(configuration);

var services = builder.Services;
services.AddSqlServerDb(dbConnectionString);
services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/providers", (IMediator mediator) => mediator.Send(new GetProviderListQuery()));

app.Run();

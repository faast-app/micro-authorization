using ApiAuth.Application;
using ApiAuth.Common;
using ApiAuth.Extensions;
using ApiAuth.Infrastructure;
using ApiAuth.Middleware;
using ApiAuth.OptionsSetup;
using ApiAuth.Persistence;
using Carter;

var builder = WebApplication.CreateBuilder(args);

var builderEnvironment = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();


builder.Services.AddSwaggerExtension();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddApplication();
builder.Services.AddPersistence();
builder.Services.AddInfrastructure();

//builder.Services.AddProblemDetails();

builder.Services.ConfigureOptions<AuthOptionsSetup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapCarter();

//app.UseExceptionHandler();

app.MapGet("/", async (context) =>
{
    context.Response.Redirect("swagger");
    await Task.FromResult(0);
});

app.Run();

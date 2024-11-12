
using FastEndpoints;
using FastEndpoints.Swagger;
using SuperNote.Application;
using SuperNote.DataAccess;
using SuperNote.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly,
    typeof(ApplicationServices).Assembly));

builder.Services.SwaggerDocument(options =>
{
    options.ShortSchemaNames = true;
});

builder.Services.AddSingleton(TimeProvider.System);

builder.Services
    .AddDomainServices()
    .AddApplicationServices()
    .AddDataAccessServices(builder.Configuration.GetValue<string>("ConnectionStrings:SuperNote"));

var app = builder.Build();
app.UseFastEndpoints(config => config.Errors.UseProblemDetails());
app.UseSwaggerGen();

app.Run();
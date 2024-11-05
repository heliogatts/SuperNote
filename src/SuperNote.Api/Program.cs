
using SuperNote.Application;
using SuperNote.DataAccess;
using SuperNote.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomainServices()
    .AddApplicationServices()
    .AddDataAccessServices();

var app = builder.Build();

app.Run();
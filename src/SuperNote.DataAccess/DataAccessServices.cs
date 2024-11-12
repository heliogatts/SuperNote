using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperNote.DataAccess.DataAccess;
using SuperNote.DataAccess.Notes;
using SuperNote.Domain.Abstractions.DataAccess;
using SuperNote.Domain.Notes;

namespace SuperNote.DataAccess;

public static class DataAccessServices
{
    private const string InMemoryDatabaseName = "Data Source=./SuperNoteDatabase.db;";
        
    public static IServiceCollection AddDataAccessServices(
        this IServiceCollection services, string? connectionString = null)
    {
        services.AddDbContext<SuperNoteContext>(options =>
        {
            options.UseSqlite(InMemoryDatabaseName);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INotesRepository, NotesRepository>();

        return services;
    }
}
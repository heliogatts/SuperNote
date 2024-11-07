using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperNote.DataAccess.DataAccess;

namespace SuperNote.DataAccess;

public static class DataAccessServices
{
    private const string InMemoryDatabaseName = "Data Source=./SuperNoteDatabase.db;";
        
    public static IServiceCollection AddDataAccessServices(
        this IServiceCollection services, string? connectionString = null)
    {
        IsConnectionStringAvailable(services, connectionString);
        return services;
    }

    #region Private Methods

    private static void IsConnectionStringAvailable(IServiceCollection services, string? connectionString)
    {
        if (connectionString is not null)
        {
            services.AddDbContext<SuperNoteContext>(options =>
            {
                options.UseSqlite(InMemoryDatabaseName);
            });
        }
        else
        {
            services.AddDbContext<SuperNoteContext>(options =>
            {
                options.UseInMemoryDatabase(InMemoryDatabaseName);
            });
        }
    }
    #endregion
    
}
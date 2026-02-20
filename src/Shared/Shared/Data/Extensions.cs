using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Data;

public static class Extensions
{
    public static WebApplication UseMigrations<TContext>(this WebApplication app) where TContext : DbContext
    {
        ApplyMigrationsAsync<TContext>(app.Services).GetAwaiter().GetResult();
        SeedDataAsync(app.Services).GetAwaiter().GetResult();
        
        return app;
    }
    
    private static async Task ApplyMigrationsAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        
        await context.Database.MigrateAsync();
    }

    private static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
        foreach (var s in seeders)
        {
            await s.SeedAsync();
        }
    }
    
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any(er => er.TargetEntry != null && er.TargetEntry.Metadata.IsOwned() && (er.TargetEntry.State == EntityState.Added || er.TargetEntry.State == EntityState.Modified));
    }
}
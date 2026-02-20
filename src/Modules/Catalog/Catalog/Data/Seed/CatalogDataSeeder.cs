using Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Catalog.Data.Seed;

public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if (!await dbContext.Products.AnyAsync())
        {
            await dbContext.Products.AddRangeAsync(new List<Product>
            {
                Product.Create(Guid.Parse("983ee487-5717-46b3-9da8-cce8b69d4f85"), "Tesla Model Y Standard Range", ["Vehicles"], "", "", 2300000),
                Product.Create(Guid.Parse("7b7d666b-f64b-47f1-8c5b-37e2bd12c812"), "Tesla Model Y Long Range", ["Vehicles"], "", "", 3540000),
                Product.Create(Guid.Parse("7aac8dba-c02c-4890-8310-20ad0a707d0c"), "Tesla Model Y Performance", ["Vehicles"], "", "", 4570000)
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
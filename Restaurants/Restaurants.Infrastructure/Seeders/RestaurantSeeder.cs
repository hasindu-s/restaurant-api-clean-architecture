using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Seeders;
internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];

        return restaurants;
    }

    public async Task Seed()
    {
        try
        {
            // Check database connection
            var canConnect = await dbContext.Database.CanConnectAsync();

            if (!canConnect)
            {
                Console.WriteLine("Cannot connect to database. Check connection string.");
                return;
            }

            // Ensure database is created
            var ensureCreated = await dbContext.Database.EnsureCreatedAsync();
            Console.WriteLine($"Database ensure created result: {ensureCreated}");

            // Check current restaurant count
            var existingCount = await dbContext.Restaurants.CountAsync();
            Console.WriteLine($"Current restaurant count in database: {existingCount}");

            if (existingCount == 0)
            {
                Console.WriteLine("No restaurants found. Adding seed data...");

                var restaurants = GetRestaurants();
                Console.WriteLine($"Adding {restaurants.Count()} restaurants to context...");

                await dbContext.Restaurants.AddRangeAsync(restaurants);

                Console.WriteLine("Saving changes to database...");
                var savedCount = await dbContext.SaveChangesAsync();
                Console.WriteLine($"Successfully seeded {savedCount} records to database");
            }
            else
            {
                Console.WriteLine($"Database already contains {existingCount} restaurants. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during seeding: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");

            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }

            throw;
        }
    }
}
using ConstructionBidPortal.API.Models;

namespace ConstructionBidPortal.API.Data
{
    public static class SeedData
    {
        public static void Initialize(BidPortalContext context)
        {
            // Check if data already exists
            if (context.Users.Any() || context.Projects.Any() || context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            // Add Categories
            var categories = new Category[]
            {
                new Category { Name = "Commercial", Description = "Commercial building projects" },
                new Category { Name = "Residential", Description = "Residential building projects" },
                new Category { Name = "Industrial", Description = "Industrial building projects" },
                new Category { Name = "Infrastructure", Description = "Infrastructure projects" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Add Users
            var users = new User[]
            {
                new User { Email = "owner@example.com", PasswordHash = "demo", FirstName = "John", LastName = "Smith", UserType = "Owner" },
                new User { Email = "contractor@example.com", PasswordHash = "demo", FirstName = "Bob", LastName = "Builder", UserType = "Contractor" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Add Projects
            var projects = new Project[]
            {
                new Project {
                    OwnerId = users[0].Id,
                    Title = "Modern Office Complex",
                    Description = "This project involves constructing a 5-story office building with underground parking...",
                    Location = "New York, NY",
                    Budget = 1200000,
                    BidDeadline = DateTime.Now.AddMonths(3),
                    Status = "Open"
                },
                new Project {
                    OwnerId = users[0].Id,
                    Title = "Luxury Home Renovation",
                    Description = "Complete renovation of a 4,500 sq ft luxury home...",
                    Location = "Chicago, IL",
                    Budget = 350000,
                    BidDeadline = DateTime.Now.AddMonths(2),
                    Status = "Open"
                }
            };

            context.Projects.AddRange(projects);
            context.SaveChanges();

            // Add Project-Category relationships
            var projectCategories = new ProjectCategory[]
            {
                new ProjectCategory { ProjectId = projects[0].Id, CategoryId = categories[0].Id },
                new ProjectCategory { ProjectId = projects[1].Id, CategoryId = categories[1].Id }
            };

            context.AddRange(projectCategories);
            context.SaveChanges();
        }
    }
}

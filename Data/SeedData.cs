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
                new Category { Name = "Commercial", Description = "Office buildings, retail spaces, restaurants, hotels" },
                new Category { Name = "Residential", Description = "Single-family homes, apartments, condominiums" },
                new Category { Name = "Industrial", Description = "Factories, warehouses, manufacturing facilities" },
                new Category { Name = "Infrastructure", Description = "Roads, bridges, utilities, transportation systems" },
                new Category { Name = "Renovation/Remodeling", Description = "Interior/exterior updates, modernization projects" },
                new Category { Name = "Electrical", Description = "Wiring, lighting systems, electrical installations" },
                new Category { Name = "Plumbing", Description = "Pipe systems, fixtures, water/sewage installations" },
                new Category { Name = "HVAC", Description = "Heating, ventilation, air conditioning systems" },
                new Category { Name = "Roofing", Description = "Roof installation, repair, replacement" },
                new Category { Name = "Concrete/Masonry", Description = "Foundation, concrete work, brickwork, stonework" },
                new Category { Name = "Carpentry", Description = "Framing, trim work, cabinetry, custom woodwork" },
                new Category { Name = "Landscaping", Description = "Site preparation, grading, outdoor aesthetics" },
                new Category { Name = "Demolition", Description = "Structure removal, site clearance" },
                new Category { Name = "Painting/Finishing", Description = "Interior/exterior painting, decorative finishes" },
                new Category { Name = "Flooring", Description = "Tile, hardwood, carpet, laminate installation" },
                new Category { Name = "Green/Sustainable", Description = "Eco-friendly, LEED certified, energy-efficient projects" }
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

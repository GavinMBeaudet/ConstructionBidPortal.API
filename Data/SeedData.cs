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

            // Add Users (10 Owners, 10 Contractors)
            var users = new User[]
            {
                new User { Email = "owner1@test.com", PasswordHash = "password1", FirstName = "Alice", LastName = "Anderson", UserType = "Owner" },
                new User { Email = "owner2@test.com", PasswordHash = "password2", FirstName = "Brian", LastName = "Baker", UserType = "Owner" },
                new User { Email = "owner3@test.com", PasswordHash = "password3", FirstName = "Cathy", LastName = "Clark", UserType = "Owner" },
                new User { Email = "owner4@test.com", PasswordHash = "password4", FirstName = "David", LastName = "Dunn", UserType = "Owner" },
                new User { Email = "owner5@test.com", PasswordHash = "password5", FirstName = "Eva", LastName = "Evans", UserType = "Owner" },
                new User { Email = "owner6@test.com", PasswordHash = "password6", FirstName = "Frank", LastName = "Foster", UserType = "Owner" },
                new User { Email = "owner7@test.com", PasswordHash = "password7", FirstName = "Grace", LastName = "Green", UserType = "Owner" },
                new User { Email = "owner8@test.com", PasswordHash = "password8", FirstName = "Henry", LastName = "Hughes", UserType = "Owner" },
                new User { Email = "owner9@test.com", PasswordHash = "password9", FirstName = "Ivy", LastName = "Irwin", UserType = "Owner" },
                new User { Email = "owner10@test.com", PasswordHash = "password10", FirstName = "Jack", LastName = "Johnson", UserType = "Owner" },
                new User { Email = "contractor1@test.com", PasswordHash = "password11", FirstName = "Kyle", LastName = "King", UserType = "Contractor" },
                new User { Email = "contractor2@test.com", PasswordHash = "password12", FirstName = "Laura", LastName = "Lewis", UserType = "Contractor" },
                new User { Email = "contractor3@test.com", PasswordHash = "password13", FirstName = "Mike", LastName = "Morris", UserType = "Contractor" },
                new User { Email = "contractor4@test.com", PasswordHash = "password14", FirstName = "Nina", LastName = "Nelson", UserType = "Contractor" },
                new User { Email = "contractor5@test.com", PasswordHash = "password15", FirstName = "Oscar", LastName = "Owens", UserType = "Contractor" },
                new User { Email = "contractor6@test.com", PasswordHash = "password16", FirstName = "Paula", LastName = "Parker", UserType = "Contractor" },
                new User { Email = "contractor7@test.com", PasswordHash = "password17", FirstName = "Quinn", LastName = "Quincy", UserType = "Contractor" },
                new User { Email = "contractor8@test.com", PasswordHash = "password18", FirstName = "Rachel", LastName = "Reed", UserType = "Contractor" },
                new User { Email = "contractor9@test.com", PasswordHash = "password19", FirstName = "Steve", LastName = "Stone", UserType = "Contractor" },
                new User { Email = "contractor10@test.com", PasswordHash = "password20", FirstName = "Tina", LastName = "Turner", UserType = "Contractor" }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Add Projects (20 projects, varied owners, budgets, locations, deadlines, statuses)
            var projects = new List<Project>();
            var rnd = new Random(42);
            string[] locations = { "New York, NY", "Chicago, IL", "Los Angeles, CA", "Houston, TX", "Miami, FL", "Seattle, WA", "Denver, CO", "Boston, MA", "Phoenix, AZ", "Atlanta, GA" };
            string[] statuses = { "Open", "Awarded", "Closed" };
            for (int i = 0; i < 20; i++)
            {
                var owner = users[rnd.Next(0, 10)];
                var status = statuses[rnd.Next(0, statuses.Length)];
                projects.Add(new Project
                {
                    OwnerId = owner.Id,
                    Title = $"Project {i + 1} - {categories[rnd.Next(categories.Length)].Name}",
                    Description = $"Detailed description for project {i + 1} in the {categories[rnd.Next(categories.Length)].Name} category.",
                    Location = locations[rnd.Next(locations.Length)],
                    Budget = rnd.Next(50000, 2000000),
                    BidDeadline = DateTime.Now.AddDays(rnd.Next(10, 120)),
                    Status = status
                });
            }
            context.Projects.AddRange(projects);
            context.SaveChanges();

            // Add Project-Category relationships (2-3 categories per project)
            var projectCategories = new List<ProjectCategory>();
            for (int i = 0; i < projects.Count; i++)
            {
                var catIndices = new HashSet<int>();
                while (catIndices.Count < rnd.Next(2, 4))
                {
                    catIndices.Add(rnd.Next(categories.Length));
                }
                foreach (var ci in catIndices)
                {
                    projectCategories.Add(new ProjectCategory { ProjectId = projects[i].Id, CategoryId = categories[ci].Id });
                }
            }
            context.AddRange(projectCategories);
            context.SaveChanges();

            // Add Bids (40 bids, random contractors, projects, amounts, timelines, proposals)
            var bids = new List<Bid>();
            for (int i = 0; i < 40; i++)
            {
                var contractor = users[10 + rnd.Next(0, 10)];
                var project = projects[rnd.Next(projects.Count)];
                bids.Add(new Bid
                {
                    ProjectId = project.Id,
                    ContractorId = contractor.Id,
                    BidAmount = rnd.Next(20000, (int)project.Budget),
                    TimelineInDays = rnd.Next(14, 180),
                    Proposal = $"Proposal for project {project.Title} by {contractor.FirstName} {contractor.LastName}.",
                    Status = "Pending",
                    DateSubmitted = DateTime.Now.AddDays(-rnd.Next(1, 60))
                });
            }
            context.Bids.AddRange(bids);
            context.SaveChanges();

            /*
            =====================
            TEST USER ACCOUNTS
            =====================
            Owners:
            owner1@test.com / password1
            owner2@test.com / password2
            owner3@test.com / password3
            owner4@test.com / password4
            owner5@test.com / password5
            owner6@test.com / password6
            owner7@test.com / password7
            owner8@test.com / password8
            owner9@test.com / password9
            owner10@test.com / password10

            Contractors:
            contractor1@test.com / password11
            contractor2@test.com / password12
            contractor3@test.com / password13
            contractor4@test.com / password14
            contractor5@test.com / password15
            contractor6@test.com / password16
            contractor7@test.com / password17
            contractor8@test.com / password18
            contractor9@test.com / password19
            contractor10@test.com / password20
            */
        }
    }
}

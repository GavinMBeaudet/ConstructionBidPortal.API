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
                new User { Email = "owner1@test.com", PasswordHash = "password1", FirstName = "Alice", LastName = "Anderson", UserType = "Owner", Address = "514 Oak Ave", City = "Los Angeles", State = "CA", Zip = "37308", Title = "Owner" },
                new User { Email = "owner2@test.com", PasswordHash = "password2", FirstName = "Brian", LastName = "Baker", UserType = "Owner", Address = "515 Oak Ave", City = "Chicago", State = "IL", Zip = "60601", Title = "Owner" },
                new User { Email = "owner3@test.com", PasswordHash = "password3", FirstName = "Cathy", LastName = "Clark", UserType = "Owner", Address = "516 Oak Ave", City = "New York", State = "NY", Zip = "10001", Title = "Owner" },
                new User { Email = "owner4@test.com", PasswordHash = "password4", FirstName = "David", LastName = "Dunn", UserType = "Owner", Address = "517 Oak Ave", City = "Houston", State = "TX", Zip = "77001", Title = "Owner" },
                new User { Email = "owner5@test.com", PasswordHash = "password5", FirstName = "Eva", LastName = "Evans", UserType = "Owner", Address = "518 Oak Ave", City = "Miami", State = "FL", Zip = "33101", Title = "Owner" },
                new User { Email = "owner6@test.com", PasswordHash = "password6", FirstName = "Frank", LastName = "Foster", UserType = "Owner", Address = "519 Oak Ave", City = "Seattle", State = "WA", Zip = "98101", Title = "Owner" },
                new User { Email = "owner7@test.com", PasswordHash = "password7", FirstName = "Grace", LastName = "Green", UserType = "Owner", Address = "520 Oak Ave", City = "Denver", State = "CO", Zip = "80201", Title = "Owner" },
                new User { Email = "owner8@test.com", PasswordHash = "password8", FirstName = "Henry", LastName = "Hughes", UserType = "Owner", Address = "521 Oak Ave", City = "Boston", State = "MA", Zip = "02101", Title = "Owner" },
                new User { Email = "owner9@test.com", PasswordHash = "password9", FirstName = "Ivy", LastName = "Irwin", UserType = "Owner", Address = "522 Oak Ave", City = "Phoenix", State = "AZ", Zip = "85001", Title = "Owner" },
                new User { Email = "owner10@test.com", PasswordHash = "password10", FirstName = "Jack", LastName = "Johnson", UserType = "Owner", Address = "523 Oak Ave", City = "Atlanta", State = "GA", Zip = "30301", Title = "Owner" },
                new User { Email = "contractor1@test.com", PasswordHash = "password11", FirstName = "Kyle", LastName = "King", UserType = "Contractor", Address = "465 Main St", City = "Los Angeles", State = "CA", Zip = "37001", LicenseNumber = "TN957723" },
                new User { Email = "contractor2@test.com", PasswordHash = "password12", FirstName = "Laura", LastName = "Lewis", UserType = "Contractor", Address = "466 Main St", City = "Chicago", State = "IL", Zip = "60602", LicenseNumber = "TN957724" },
                new User { Email = "contractor3@test.com", PasswordHash = "password13", FirstName = "Mike", LastName = "Morris", UserType = "Contractor", Address = "727 Main St", City = "Los Angeles", State = "CA", Zip = "37709", LicenseNumber = "TN113650" },
                new User { Email = "contractor4@test.com", PasswordHash = "password14", FirstName = "Nina", LastName = "Nelson", UserType = "Contractor", Address = "728 Main St", City = "Houston", State = "TX", Zip = "77002", LicenseNumber = "TN113651" },
                new User { Email = "contractor5@test.com", PasswordHash = "password15", FirstName = "Oscar", LastName = "Owens", UserType = "Contractor", Address = "729 Main St", City = "Miami", State = "FL", Zip = "33102", LicenseNumber = "TN113652" },
                new User { Email = "contractor6@test.com", PasswordHash = "password16", FirstName = "Paula", LastName = "Parker", UserType = "Contractor", Address = "730 Main St", City = "Seattle", State = "WA", Zip = "98102", LicenseNumber = "TN113653" },
                new User { Email = "contractor7@test.com", PasswordHash = "password17", FirstName = "Quinn", LastName = "Quincy", UserType = "Contractor", Address = "731 Main St", City = "Denver", State = "CO", Zip = "80202", LicenseNumber = "TN113654" },
                new User { Email = "contractor8@test.com", PasswordHash = "password18", FirstName = "Rachel", LastName = "Reed", UserType = "Contractor", Address = "732 Main St", City = "Boston", State = "MA", Zip = "02102", LicenseNumber = "TN113655" },
                new User { Email = "contractor9@test.com", PasswordHash = "password19", FirstName = "Steve", LastName = "Stone", UserType = "Contractor", Address = "733 Main St", City = "Phoenix", State = "AZ", Zip = "85002", LicenseNumber = "TN113656" },
                new User { Email = "contractor10@test.com", PasswordHash = "password20", FirstName = "Tina", LastName = "Turner", UserType = "Contractor", Address = "734 Main St", City = "Atlanta", State = "GA", Zip = "30302", LicenseNumber = "TN113657" }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Reload users from DB and map by email
            var ownerEmails = new[] {
                "owner1@test.com", "owner2@test.com", "owner3@test.com", "owner4@test.com", "owner5@test.com",
                "owner6@test.com", "owner7@test.com", "owner8@test.com", "owner9@test.com", "owner10@test.com"
            };
            var dbOwners = context.Users.Where(u => ownerEmails.Contains(u.Email)).ToList();
            var ownerMap = dbOwners.ToDictionary(u => u.Email, u => u.Id);

            // Add Projects (20 projects, varied owners, budgets, locations, deadlines, statuses)
            var projects = new List<Project>();
            var rnd = new Random(42);
            string[] locations = { "New York, NY", "Chicago, IL", "Los Angeles, CA", "Houston, TX", "Miami, FL", "Seattle, WA", "Denver, CO", "Boston, MA", "Phoenix, AZ", "Atlanta, GA" };
            string[] statuses = { "Open", "In Progress" };
            for (int i = 0; i < 20; i++)
            {
                // Pick a random owner email and get correct DB ID
                var ownerEmail = ownerEmails[rnd.Next(0, ownerEmails.Length)];
                var ownerId = ownerMap[ownerEmail];
                var status = statuses[rnd.Next(0, statuses.Length)];
                projects.Add(new Project
                {
                    OwnerId = ownerId,
                    Title = $"Project {i + 1} - {categories[rnd.Next(categories.Length)].Name}",
                    Description = $"Detailed description for project {i + 1} in the {categories[rnd.Next(categories.Length)].Name} category.",
                    Location = locations[rnd.Next(locations.Length)],
                    Budget = rnd.Next(50000, 2000000),
                    BidDeadline = DateTime.Now.AddDays(rnd.Next(10, 120)),
                    Status = status,
                    ProjectNumber = $"TN-{i + 1:000}",
                    Address = $"{rnd.Next(100, 999)} Elm St",
                    City = locations[i % locations.Length].Split(',')[0],
                    State = locations[i % locations.Length].Split(',')[1].Trim(),
                    Zip = (37000 + rnd.Next(0, 999)).ToString()
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
                    FinalContractPrice = rnd.Next(20000, (int)project.Budget),
                    CompletionDays = rnd.Next(30, 180).ToString(),
                    Proposal = $"Proposal for project {project.Title} by {contractor.FirstName} {contractor.LastName}.",
                    Status = "Pending",
                    DateSubmitted = DateTime.Now.AddDays(-rnd.Next(1, 60)),
                    ContractorName = contractor.FirstName + " " + contractor.LastName,
                    ContractorAddress = $"{rnd.Next(100, 999)} Main St",
                    ContractorCity = project.Location.Split(',')[0],
                    ContractorState = project.Location.Split(',')[1].Trim(),
                    ContractorZip = (37000 + rnd.Next(0, 999)).ToString(),
                    ContractorLicense = $"TN{rnd.Next(100000, 999999)}",
                    OwnerName = users[project.OwnerId - 1].FirstName + " " + users[project.OwnerId - 1].LastName,
                    OwnerAddress = $"{rnd.Next(100, 999)} Oak Ave",
                    OwnerCity = project.Location.Split(',')[0],
                    OwnerState = project.Location.Split(',')[1].Trim(),
                    OwnerZip = (37000 + rnd.Next(0, 999)).ToString(),
                    LenderName = "First National Bank",
                    LenderAddress = $"{rnd.Next(100, 999)} Bank St",
                    LenderCity = project.Location.Split(',')[0],
                    LenderState = project.Location.Split(',')[1].Trim(),
                    LenderZip = (37000 + rnd.Next(0, 999)).ToString(),
                    ProjectNumber = $"TN-{project.Id:000}",
                    ProjectAddress = $"{rnd.Next(100, 999)} Elm St",
                    ProjectCity = project.Location.Split(',')[0],
                    ProjectState = project.Location.Split(',')[1].Trim(),
                    ProjectZip = (37000 + rnd.Next(0, 999)).ToString(),
                    ProjectDescription = project.Description,
                    OtherContractDocs = "General conditions, drawings, specifications",
                    WorkInvolved = "Site prep, foundation, framing, roofing, finishing",
                    CommencementType = "notice",
                    CommencementDays = rnd.Next(5, 30).ToString(),
                    CommencementOther = "",
                    CompletionType = "days",
                    CompletionOther = "",
                    ProgressRetentionPercent = "5",
                    ProgressRetentionDays = "30",
                    FinalPaymentDays = "15",
                    TerminationDate = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd"),
                    ProposalDate = DateTime.Now.AddDays(-rnd.Next(1, 60)).ToString("yyyy-MM-dd"),
                    WarrantyYears = "1",
                    AdditionalProvisions = "Standard warranty applies",
                    ContractorSignaturesJson = System.Text.Json.JsonSerializer.Serialize(new[] { new { Name = contractor.FirstName + " " + contractor.LastName, Title = "President", Date = DateTime.Now.ToString("yyyy-MM-dd") } }),
                    OwnerSignaturesJson = System.Text.Json.JsonSerializer.Serialize(new[] { new { Name = users[project.OwnerId - 1].FirstName + " " + users[project.OwnerId - 1].LastName, Title = "Owner", Date = DateTime.Now.ToString("yyyy-MM-dd") } })
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

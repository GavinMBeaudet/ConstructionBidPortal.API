# Construction Bid Portal API

## Overview
This repository contains the backend API for the Construction Bid Portal, built with ASP.NET Core. It is part of a monorepo that also includes a React frontend (see the `client/` directory).

## Architecture
- **Framework:** ASP.NET Core Web API (.NET 8)
- **Project Structure:**
  - `Endpoints/`: Minimal API endpoints grouped by resource (e.g., Auth, Projects, Bids, Categories, Users)
  - `Models/`: Entity Framework Core models representing database tables
  - `DTOs/`: Data Transfer Objects for API requests and responses
  - `Data/`: Database context (`BidPortalContext.cs`) and seed data
  - `Migrations/`: Entity Framework Core migrations
  - `Properties/`: Launch settings
- **Database:** Entity Framework Core (code-first), migrations managed in `Migrations/`
- **Authentication:** JWT-based authentication

## Data Flow
- **Frontend** communicates with the API via HTTP requests (see `client/src/services/apiService.js` and `authService.js`)
- **API** handles authentication, project management, bidding, user management, and category management
- **DTOs** are used to decouple internal models from API contracts

## Developer Workflow
### Build & Run
- **Build API:**
  ```bash
  cd construction-bid-portal-server/api/ConstructionBidPortal.API
  dotnet build 
  ```
- **Run API (development):**
  ```bash
  cd construction-bid-portal-server/api/ConstructionBidPortal.API
  dotnet run
  ```
- **Build Client:**
  ```bash
  cd construction-bid-portal-server/client
  npm install
  npm run dev
  ```

### Database Migrations
- **Add Migration:**
  ```bash
  dotnet ef migrations add <MigrationName> --project construction-bid-portal-server/api/ConstructionBidPortal.API
  ```
- **Update Database:**
  ```bash
  dotnet ef database update --project construction-bid-portal-server/api/ConstructionBidPortal.API
  ```

## Conventions & Patterns
- **Minimal APIs:** Endpoints are grouped by resource in the `Endpoints/` folder
- **DTO Mapping:** Models are mapped to DTOs for API responses
- **Protected Routes:** JWT authentication is enforced on protected endpoints
- **Configuration:** Environment-specific settings in `appsettings.Development.json`

## Integration Points
- **API <-> Client:** All API calls are routed through the frontend's `apiService.js`
- **Authentication:** JWT tokens are issued and validated by the API, and managed on the client

## Extending the API
- **Add a New Resource:**
  1. Create a model in `Models/`
  2. Create a DTO in `DTOs/`
  3. Add an endpoint in `Endpoints/`
  4. Update the context and add a migration if needed

## Tips
- Keep DTOs and models in sync between API and client
- Use VS Code tasks for consistent builds
- Check `appsettings.Development.json` for local configuration

---
For more details, see the code in each folder and the comments in the source files.

# Copilot Instructions for Construction Bid Portal

## Big Picture Architecture
- **Monorepo:** Contains both ASP.NET Core backend (`api/ConstructionBidPortal.API`) and React frontend (`client/`).
- **Backend:** Minimal API endpoints grouped by resource (`Endpoints/`), Entity Framework Core models (`Models/`), DTOs for API contracts (`DTOs/`), and migrations (`Migrations/`).
- **Frontend:** React app with pages in `src/pages/`, reusable components in `src/components/`, and API communication via `src/services/apiService.js` and `authService.js`.
- **Data Flow:** Frontend communicates with backend via HTTP; backend uses DTOs to decouple internal models from API responses.

## Developer Workflows
- **Build Backend:**
  - Use VS Code task `build` or run: `dotnet build api/ConstructionBidPortal.API/ConstructionBidPortal.API.csproj`
- **Run Backend (dev):**
  - Use VS Code task `watch` or run: `dotnet watch run --project api/ConstructionBidPortal.API/ConstructionBidPortal.API.csproj`
- **Build/Run Frontend:**
  - Use VS Code tasks or run: `npm install` then `npm run dev` in `client/`
- **Database Migrations:**
  - Add: `dotnet ef migrations add <MigrationName> --project api/ConstructionBidPortal.API`
  - Update: `dotnet ef database update --project api/ConstructionBidPortal.API`

## Project-Specific Conventions
- **Minimal APIs:** Endpoints are grouped by resource in `Endpoints/` (e.g., `AuthEndpoints.cs`, `ProjectsEndpoints.cs`).
- **DTO Usage:** All API requests/responses use DTOs from `DTOs/` to avoid leaking internal models.
- **JWT Authentication:** Protected endpoints require JWT; client manages tokens via `authService.js`.
- **Configuration:** Use `appsettings.Development.json` for local settings.
- **Frontend Routing:** React Router is used for navigation; protected routes via `PrivateRoute.jsx`.

## Integration Points
- **API <-> Client:** All HTTP requests from frontend go through `apiService.js` and `authService.js`.
- **Database:** EF Core migrations managed in `Migrations/`; context in `Data/BidPortalContext.cs`.
- **Signature Capture:** Owner acceptance modal in frontend includes signature capture for bid awards.

## Extending the System
- **Add a Resource:**
  1. Create model in `Models/`
  2. Create DTO in `DTOs/`
  3. Add endpoint in `Endpoints/`
  4. Update context and add migration if needed
  5. Update frontend service and page as needed

## Examples
- **API Endpoint:** See `Endpoints/BidsEndpoints.cs` for bid-related routes.
- **Frontend Service:** See `client/src/services/apiService.js` for API calls.
- **Protected Route:** See `client/src/components/PrivateRoute.jsx` for auth logic.

## Tips
- Use VS Code tasks for builds and dev servers.
- Keep DTOs and models in sync between backend and frontend.
- Check README files in `client/` and `api/` for more details.

---
For questions or unclear conventions, review comments in source files or ask for clarification.

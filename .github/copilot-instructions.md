# Copilot Instructions for construction-bid-portal-server

## Project Overview
- **Monorepo** with two main parts:
  - `api/ConstructionBidPortal.API/`: ASP.NET Core Web API (C#)
  - `client/`: React frontend (Vite, JavaScript)

## Architecture & Data Flow
- **API**: Handles authentication, projects, bids, categories, and users. Organized by feature in `Endpoints/`, `Models/`, `DTOs/`, and `Data/`.
- **Frontend**: React app with pages in `src/pages/`, shared components in `src/components/`, and API calls in `src/services/`.
- **Communication**: Frontend uses `apiService.js` and `authService.js` to interact with the backend API.

## Developer Workflows
- **Build API**: `dotnet build api/ConstructionBidPortal.API/ConstructionBidPortal.API.csproj`
- **Run API (dev)**: `dotnet watch run --project api/ConstructionBidPortal.API/ConstructionBidPortal.API.csproj`
- **Build Client**: `npm install` then `npm run dev` in `client/`
- **Tasks**: Use VS Code tasks for common commands (see `.vscode/tasks.json`)

## Conventions & Patterns
- **API Endpoints**: Grouped by resource in `Endpoints/`. Use minimal APIs pattern.
- **Data Models**: In `Models/`, mapped to DTOs in `DTOs/` for API responses.
- **Database**: Entity Framework Core migrations in `Migrations/`. Context in `Data/BidPortalContext.cs`.
- **Frontend Routing**: React Router, with protected routes via `PrivateRoute.jsx`.
- **State Management**: Context API for auth (`contexts/AuthContext.jsx`).
- **Modals**: Use `ConfirmModal.jsx` for confirmations.

## Integration Points
- **API <-> Client**: All API calls go through `src/services/apiService.js`.
- **Auth**: JWT-based, handled in both API and `authService.js`.

## Examples
- Add a new API resource: create model, DTO, endpoint, and update context/migrations.
- Add a new page: create in `src/pages/`, add route in `App.jsx`.

## Tips
- Keep API and client in sync for DTO changes.
- Use VS Code tasks for consistent builds.
- Check `appsettings.Development.json` for local config.

---
For questions, review the structure above and check for patterns in the relevant folders.
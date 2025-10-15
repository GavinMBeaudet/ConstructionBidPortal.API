# Construction Bid Portal - AI Coding Guidelines

## Architecture Overview
This is a full-stack construction bidding platform with ASP.NET Core Minimal API backend and React frontend.

**Backend Structure:**
- `api/ConstructionBidPortal.API/` - Main API project using .NET 8 Minimal APIs
- Entity Framework Core with SQLite database
- Domain models: `User` (Owner/Contractor), `Project`, `Bid`, `Category`
- DTOs for API request/response separation
- Extension method pattern for endpoint registration

**Frontend Structure:**
- `client/` - React 19 app with Vite build system
- React Router for SPA navigation
- Context API for authentication state management
- Service layer pattern for API communication

## Key Conventions & Patterns

### Authentication
- Session-based auth (not JWT) - user data stored in localStorage
- User types: `"Owner"` or `"Contractor"` (string literals)
- Passwords currently stored as plain text (marked TODO for BCrypt hashing)
- Auth context provides `user`, `login()`, `register()`, `logout()` methods

### API Design
- RESTful endpoints under `/api/` prefix
- Minimal API style with extension methods (e.g., `app.MapAuthEndpoints()`)
- SQLite database auto-created on startup with seed data
- CORS configured for GitHub Pages (`https://gavinmbeaudet.github.io`) and local dev
- JSON serialization: camelCase, ignore cycles, max depth 128

### Database Relationships
- Projects belong to Owners, have many Bids and Categories
- Bids belong to Projects and Contractors
- Many-to-many Projects↔Categories via `ProjectCategory` junction table
- Composite primary key on `ProjectCategory` (ProjectId, CategoryId)

### Status Values
- Projects: `"Open"` (default)
- Bids: `"Submitted"` (default), can be awarded

### Development Workflow
- **API**: Runs on `http://localhost:5090`, Swagger at `/swagger`
- **Frontend**: Runs on `http://localhost:3000`, proxies `/api/*` to API
- **Database**: SQLite file `ConstructionBidPortal.db` created automatically
- **Build**: Use VS Code tasks - `build` (API), `npm: install - client`, `npm: dev - client`

### File Organization Examples
- **Models**: `api/ConstructionBidPortal.API/Models/` - EF entities with navigation properties
- **DTOs**: `api/ConstructionBidPortal.API/DTOs/` - Request/response contracts
- **Endpoints**: `api/ConstructionBidPortal.API/Endpoints/` - API route definitions
- **Services**: `client/src/services/` - API client functions
- **Pages**: `client/src/pages/` - React route components
- **Context**: `client/src/contexts/` - React context providers

### Common Patterns
- **API Responses**: Use `Results.Ok()`, `Results.Created()`, `Results.Conflict()`, etc.
- **Error Handling**: Throw descriptive error messages from service functions
- **Navigation**: Protected routes use `<PrivateRoute>` wrapper component
- **Data Fetching**: Async/await with try/catch in React components
- **State Management**: Auth context for user state, local component state for UI

### Deployment Notes
- Frontend configured for GitHub Pages deployment
- API expects CORS from `https://gavinmbeaudet.github.io`
- Database file included in `.gitignore` (recreated on startup)

When modifying this codebase, maintain the separation between Owner and Contractor user types, ensure proper navigation property loading in EF queries, and follow the established DTO pattern for API contracts.</content>
<parameter name="filePath">c:\Users\King Vitaman\workspace\construction-bid-portal-server\.github\copilot-instructions.md
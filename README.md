# EventRegistrationAPI

## Description
A RESTful Event Registration System API built with ASP.NET Core Web API. 
The system allows users to create events, register for them, view all events 
with seat availability, and cancel registrations. It handles real-world 
constraints such as limited seats, duplicate registrations, and race conditions.

## Tech Stack
- **Language:** C#
- **Framework:** ASP.NET Core Web API (.NET 10)
- **ORM:** Entity Framework Core
- **Database:** SQLite

## Architecture
This project follows a 3-layer architecture to separate concerns and keep 
the codebase clean and maintainable:
- **Controllers** — Handle HTTP requests and responses only
- **Services** — Contain all business logic and validation rules
- **Repositories** — Handle all database operations via EF Core

## Folder Structure

```
EventRegistrationAPI/
├── Controllers/
├── Data/
├── DTOs/
├── Exceptions/
├── Migrations/
├── Models/
├── Repositories/
│   ├── Interfaces/
│   └── Implementations/
└── Services/
    ├── Interfaces/
    └── Implementations/
```
## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Git

### Run Locally
1. Clone the repository
   git clone https://github.com/abdullahShahzad-512/B0626--Muhammad-Abdullah-Shahzad--Innovaxel--Backend-Intern.git

2. Navigate to project folder
   cd EventRegistrationAPI

3. Apply database migrations
   dotnet ef database update

4. Run the project
   dotnet run

5. Open Swagger UI
   https://localhost:{port}/swagger

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/events | Create a new event |
| GET | /api/events | Get all events (supports ?upcomingOnly=true&sortByDate=true) |
| POST | /api/registrations | Register a user for an event |
| DELETE | /api/registrations/{id} | Cancel a registration |

## Design Decisions

- **SQLite** was chosen for simplicity and portability — no separate 
  database server needed, data persists between runs
- **3-Layer Architecture** keeps business logic separate from data access 
  and HTTP concerns, making the code easier to maintain and test
- **Race conditions** are handled using EF Core database transactions 
  in the registration flow to prevent overbooking
- **Custom Exceptions** (NotFoundException, ConflictException, 
  BadRequestException) are used to return proper HTTP status codes 
  for every error scenario
- **DTOs** are used to separate API contracts from database models, 
  avoiding exposing internal data structures
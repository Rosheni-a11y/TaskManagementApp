# NexTick — Task Management App

A full-stack task management application built as part of the mobile team internship assignment. It includes a .NET Web API backend, a Vue.js frontend, and a .NET MAUI mobile app that loads the frontend via WebView.

## Tech Stack

**Backend**
- .NET 10 Web API
- ADO.NET (raw SQL, no ORM)
- SQL Server
- JWT Authentication
- SQL queries stored separately in XML files

**Frontend**
- Vue.js 3 (Options API)
- TypeScript
- Vue Router
- Tailwind CSS

**Mobile**
- .NET MAUI
- WebView integration
- Native navigation + custom splash screen

## Features

- User sign up, login, and password reset
- View all users, select a user to manage their tasks
- Create, edit (title/description/due date/status), and delete tasks
- Task search and status filtering
- Dashboard with task statistics (All / Pending / In Progress / Completed)
- JWT-protected task endpoints (create, update, delete)
- Mobile app with native splash screen, top bar, and native "About" page alongside the WebView

## Project Structure

```
TaskManagementApp/
├── backend/
│   └── TaskManager.Api/
│       ├── Controllers/
│       ├── Services/
│       ├── Repositories/
│       ├── Models/
│       ├── Queries/          (SQL stored in XML)
│       └── Properties/launchSettings.json
├── frontend/
│   └── src/
│       ├── views/
│       ├── components/
│       ├── services/api.ts
│       ├── router/
│       └── store.ts
├── mobile/
│   └── PeopleTaskMobile/
│       ├── MainPage.xaml
│       ├── AppShell.xaml
│       └── Platforms/Android/
└── Postman/
    └── TaskManagementApp.postman_collection.json
```

## Prerequisites

- .NET 10 SDK
- SQL Server (local instance)
- Node.js (v22.18+ or v24.12+)
- Visual Studio 2022 (for backend and mobile)
- VS Code (recommended for frontend)
- Android device + USB cable (for mobile testing) with USB debugging enabled
- `adb` (Android Platform Tools) available on your machine

## Setup Instructions

### 1. Database

Run the following script against your local SQL Server instance to create the schema and seed some sample data:

```sql
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CreatedDate DATETIME DEFAULT GETDATE(),
    Password NVARCHAR(255) NOT NULL DEFAULT 'changeme123'
);

CREATE TABLE Tasks (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL,
    UserId INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    DueDate DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Sample seed data (optional)
INSERT INTO Users (Name, Email, Password) VALUES 
('Rosheni Bolonne', 'rosheni@gmail.com', '12345'),
('Amal Fernando', 'Amal@gmail.com', '12345');

INSERT INTO Tasks (Title, Description, Status, UserId, DueDate) VALUES 
('Design homepage', 'Create mockups', 'Pending', 1, '2026-07-15'),
('Write API docs', 'Document endpoints', 'In Progress', 1, '2026-07-10'),
('Fix bugs', 'Resolve reported issues', 'Completed', 2, '2026-07-05');
```

`Email` is a unique constraint, so each user must have a distinct email address.

Update the connection string in `backend/TaskManager.Api/appsettings.json` under `ConnectionStrings:DefaultConnection` to match your SQL Server instance.

### 2. Backend

```
cd backend/TaskManager.Api
dotnet restore
dotnet run
```

The API runs on:
- HTTP: `http://localhost:5071`
- HTTPS: `https://localhost:7158`

> Note: HTTP (port 5071) is used for local testing since the mobile WebView does not trust the local self-signed HTTPS certificate.

### 3. Frontend

```
cd frontend
npm install
npm run dev -- --host
```

Runs on `http://localhost:5173`. The `--host` flag exposes it on the network, which is required for mobile device testing.

### 4. Mobile (Android)

1. Connect your Android phone via USB with USB debugging enabled.
2. Forward the required ports so the phone can reach your local servers:
   ```
   adb reverse tcp:5173 tcp:5173
   adb reverse tcp:5071 tcp:5071
   ```
3. Open the `mobile/PeopleTaskMobile` project in Visual Studio.
4. Build and deploy to your connected device.

> These `adb reverse` commands must be re-run every time the phone is reconnected or the adb server restarts, since the port forwarding does not persist across sessions.

## API Endpoints

See the Postman collection at `Postman/TaskManagementApp.postman_collection.json` for the full set of requests, or the summary below.

| Method | Endpoint | Auth Required | Description |
|--------|----------|:---:|---|
| GET | `/api/users` | No | Get all users |
| POST | `/api/users` | No | Create a new user |
| POST | `/api/auth/login` | No | Log in, returns JWT token |
| POST | `/api/auth/reset-password` | No | Reset a user's password by email |
| GET | `/api/tasks` | No | Get all tasks (used for dashboard stats) |
| GET | `/api/tasks/{userId}` | No | Get tasks for a specific user |
| POST | `/api/tasks` | Yes | Create a task |
| PUT | `/api/tasks/{id}/full` | Yes | Update a task's title, description, status, due date |
| DELETE | `/api/tasks/{id}` | Yes | Delete a task |

For protected endpoints, include the header:
```
Authorization: Bearer <token>
```
(token obtained from the login response)

## Using the Postman Collection

1. Import `Postman/TaskManagementApp.postman_collection.json` into Postman.
2. Run the **Login** request first with a valid email/password, and copy the returned token.
3. For any request marked as requiring auth, paste the token into the Authorization tab (Bearer Token).
4. Make sure the backend is running on `http://localhost:5071` before testing.

## Known Notes

- HTTPS redirection is intentionally left disabled in `Program.cs` for local development, since the mobile WebView does not trust the local self-signed certificate. In a production deployment this would use a properly signed HTTPS certificate.
- Task ownership is tracked via `UserId` on the Tasks table (normalized design); no duplicated username fields are stored on tasks.

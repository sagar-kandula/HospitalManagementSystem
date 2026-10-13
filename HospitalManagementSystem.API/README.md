# Hospital Management System

## Project Overview
The Hospital Management System is a backend application developed using ASP.NET Core Web API and SQL Server. It manages patient information, doctor details, and appointments with authentication and authorization.

## Technologies Used
- C#
- ASP.NET Core Web API
- SQL Server
- ADO.NET
- JWT Authentication
- Swagger / OpenAPI
- Git and GitHub

## Features
- Patient management (CRUD operations)
- Doctor management (CRUD operations)
- Appointment management (CRUD operations)
- User registration and login
- JWT-based authentication
- Role-based authorization for Admin and User
- DTOs and input validation
- Repository and Service layers
- API testing using Swagger

## API Modules

### Authentication
- `POST /api/Auth/register` — Register a user
- `POST /api/Auth/login` — Log in and receive a JWT token

### Patients
- `/api/Patient` — Patient management endpoints

### Doctors
- `/api/Doctor` — Doctor management endpoints

### Appointments
- `/api/Appointment` — Appointment management endpoints

Refer to Swagger for the available HTTP methods and request formats.

## Database
The application uses SQL Server with a database named `HospitalDB`.

Configure the connection string in `appsettings.json` according to your local SQL Server setup. Configure the JWT signing key using ASP.NET Core User Secrets or a secure environment variable.

## Run the Project
1. Clone the repository.
2. Open the solution or project in Visual Studio.
3. Configure the database connection string and JWT key.
4. Ensure the required database tables exist.
5. Run the application.
6. Open the Swagger URL displayed by the application to test the APIs.

## Security
- Passwords are stored as hashes.
- JWT tokens are used for authenticated API requests.
- Patient deletion requires the Admin role.
- Do not commit passwords, JWT signing keys, or other secrets to GitHub.

## Author
Kandula Krupasagar

GitHub: https://github.com/sagar-kandula
# Library Management System (LMS)

A comprehensive system for managing library resources, including books, students, courses, and borrowing records.

## Features

- User authentication with JWT
- Book management (add, edit, delete, search)
- Student management
- Course management
- Book issuing and returning
- Fine calculation for late returns
- Comprehensive reporting

## Technology Stack

### Backend
- ASP.NET Core 10.0
- Entity Framework Core 9.0
- PostgreSQL database
- JWT Authentication
- Swagger/OpenAPI documentation

### Frontend
- WPF (Windows Presentation Foundation)
- .NET 10.0

## Project Setup

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL server
- Visual Studio 2023 or Visual Studio Code

### Backend Setup

1. Navigate to the backend directory
```bash
cd LMS\backend
```

2. Create a `.env` file in the root of the backend directory with the following variables:

| Variable | Description | Default |
|----------|-------------|---------|
| JWT_SECRET | Secret key for JWT token signing | N/A (Required) |
| AWS_ACCESS_KEY_ID | AWS access key for S3 storage | N/A (Required for file uploads) |
| AWS_SECRET_ACCESS_KEY | AWS secret access key | N/A (Required for file uploads) |
| AWS_REGION | AWS region for S3 bucket | N/A (Required for file uploads) |
| S3_BUCKET | S3 bucket name for file storage | N/A (Required for file uploads) |
| book_expires_in_days | Default loan period in days | 14 |

3. Update the database connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=lms;Username=your_username;Password=your_password"
}
```

4. Apply the database migrations:
```bash
dotnet ef database update
```

5. Run the backend application:
```bash
dotnet run
```

The backend API will be available at:
- HTTP: http://localhost:5056
- HTTPS: https://localhost:7092

### Frontend Setup

1. Navigate to the frontend directory
```bash
cd LMS\frontend
```

2. Build and run the frontend application:
```bash
dotnet build
dotnet run
```

## API Documentation

The API is documented using Swagger/OpenAPI. Once the backend is running, you can access the Swagger UI at:

- http://localhost:5056/swagger (if running in development mode)

### Authentication

Most API endpoints require authentication. To authenticate:

1. Use the `/Librarian/login` endpoint to obtain a JWT token
2. Include the token in all subsequent requests in the Authorization header:
Authorization: Bearer your_token_here

Database Schema
The system includes the following main entities:

Librarian: System administrators
Students: Library members
Books: Library resources
Courses: Academic courses
Records: Book borrowing records

License
MIT License ```
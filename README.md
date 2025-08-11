# MessagingApp

This repository contains the codebase for the MessagingApp platform.

## Backend Services

The backend is composed of ASP.NET Core microservices. The initial `AuthService` exposes user registration and login endpoints backed by a SQLite database and can be found under `src/AuthService`.

### Running the authentication service

```
cd src/AuthService
# Requires the .NET 8 SDK
dotnet run
```

The service exposes:

- `POST /api/auth/register` accepting `username`, `email`, `phoneNumber` and `password`.
- `POST /api/auth/login` accepting an `identifier` (username, email or phone number) and `password` and returning a dummy token.

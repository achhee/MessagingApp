# MessagingApp

This repository contains the codebase for the MessagingApp platform.

## Backend Services

The backend is composed of ASP.NET Core microservices. The initial `AuthService` provides a placeholder authentication API and can be found under `src/AuthService`.

### Running the authentication service

```
cd src/AuthService
# Requires the .NET 8 SDK
 dotnet run
```

The service exposes a `POST /api/auth/login` endpoint which currently returns a dummy token.

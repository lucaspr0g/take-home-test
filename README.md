# Fundo Loan Management System

A simple full-stack application for managing loans.

## Stack
- **Backend:** ASP.NET Core Web API + SQL Server (Docker)
- **Frontend:** Angular

## Prerequisites
- Docker
- .NET 6 SDK
- Node.js (v16+)
- npm
- Angular CLI (`npm install -g @angular/cli`)

## Getting Started

### 1. Clone the repository
```sh
git clone <repo-url>
```

### 2. Start the backend (API + Database)
```sh
cd backend/src
docker-compose up --build
```
- API: http://localhost:60992
- SQL Server: localhost:1433

### 3. Start the frontend
```sh
cd frontend
npm install
ng serve
```
- App: http://localhost:4200

The frontend expects the backend running at `http://localhost:60992/`.

## Project Structure
- **Backend:** API, business logic, data, tests, Docker setup
- **Frontend:** Main component, models, UI, styles

## Useful Commands
- Run backend tests:
  ```sh
  dotnet test Fundo.Services.Tests/Fundo.Services.Tests.csproj
  ```
- Run API locally (no Docker):
  ```sh
  dotnet run --project Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj
  ```

---

- The database is initialized automatically when Docker starts.
- The frontend authenticates with a ClientId and fetches a token before loading loans.

## About the development

I tried to create a simple application on Clean architecture and following some best practices of software development. If I had more time, I would improve some features to demonstrate more knoledge, like creating some use case rules and applying properly their validations, some logs for helping tracing application problems and also create more accuracy unit test cases.
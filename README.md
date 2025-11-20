# 📊 Finhiz — Social Platform for Stock Market Enthusiasts

Finhiz is a social-media–style platform for traders and investors to **manage portfolios**, **track equities**, and **share insights** through posts, comments, and discussions.  
The backend is built using **ASP.NET Core Web API**, featuring **Identity**, **JWT Authentication**, and **Claims-based Authorization**.

---

## 🚀 Features

### 🔐 Authentication & Authorization
- ASP.NET Core Identity user management  
- JWT authentication (Access Token + optional Refresh Token)  
- Claims-based authorization for fine-grained control  
- Role-based authorization (Admin / User)  

### 🧾 Portfolio Management
- Add, view, and track stocks  
- Watchlists  
- Gain/loss tracking  

### 📝 Social Features
- Create posts  
- Comment & reply  
- Like/react  
- Follow users  
- Personal user profiles  

### 📈 Market Tracking
- Lookup equities  
- Historical stock data  
- Search functionality  

---

## 🛠️ Tech Stack
- ASP.NET Core 8 Web API  
- ASP.NET Core Identity  
- JWT Authentication  
- Claims-based Authorization  
- Entity Framework Core  
- SQL Server  
- Repository & Service Layer Pattern  
- Swagger / OpenAPI  

---

## 📁 Project Structure

StockMarket.API/
- Controllers/ # API endpoints (Auth, Stocks, Portfolio, etc.)
- Data/ # DbContext, database configuration
- Dtos/ # Request/Response DTOs
- Extensions/ # Extension methods (Service registrations, JWT config)
- Interfaces/ # Abstractions for services & repositories
- Mappers/ # Manual Mapping
- Migrations/ # EF Core migration files
- Models/ # Entity models (User, Stock, Portfolio, etc.)
- Repository/ # Repository implementations
- Service/ # Business logic + TokenService
- └── TokenService.cs
- appsettings.json # Database + JWT configuration
- Program.cs # Application entry point (DI setup, middleware)
- StockMarket.API.http # HTTP test file for API calls


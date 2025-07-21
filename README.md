# AdminJox - Domain-Driven Design API Solution

A comprehensive .NET 8.0 Web API solution built with Domain-Driven Design (DDD) principles, featuring a clean architecture with multiple layers and modern development practices.

## 🏗️ Architecture Overview

This solution follows DDD principles with a clean architecture structure:

- **Api**: Web API layer with controllers, middleware, and configuration
- **Application**: Application services, commands, handlers, and interfaces
- **Domain**: Core business logic, entities, value objects, and domain services
- **Infrastructure**: Data access, external services, and infrastructure concerns
- **Utilities.Artifacts**: Shared utilities and packages

## 🚀 Features

- **Clean Architecture**: Separation of concerns with DDD principles
- **CQRS Pattern**: Command Query Responsibility Segregation implementation
- **Multi-Database Support**: SQL Server, MongoDB, and Redis integration
- **Azure Integration**: Cosmos DB, SignalR, Application Insights, and more
- **AI Integration**: OpenAI and Azure Cognitive Services support
- **Authentication & Authorization**: Built-in security features
- **Swagger Documentation**: API documentation with Swagger/OpenAPI
- **Health Checks**: Application health monitoring
- **Containerization**: Docker support for deployment

## 🛠️ Technology Stack

- **.NET 8.0**: Latest .NET framework
- **ASP.NET Core**: Web API framework
- **Entity Framework Core**: ORM for SQL Server
- **MongoDB**: Document database support
- **Redis**: Caching and session storage
- **Azure Services**: Cloud integration
- **Docker**: Containerization
- **Swagger/OpenAPI**: API documentation

## 📋 Prerequisites

- .NET 8.0 SDK
- SQL Server (or Azure SQL Database)
- MongoDB (or Azure Cosmos DB with MongoDB API)
- Redis (or Azure Cache for Redis)
- Docker (optional, for containerization)

## ⚙️ Configuration

### Connection Strings

Update the connection strings in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnectionSqlServer": "Your SQL Server connection string",
    "DefaultConnectionMongo": "Your MongoDB connection string", 
    "DefaultConnectionRedis": "Your Redis connection string"
  }
}
```

### Azure Services Configuration

Configure Azure services in `appsettings.json`:

- Application Insights
- Key Vault
- SignalR Service
- Cosmos DB
- OpenAI Services
- Cognitive Services

## 🏃‍♂️ Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/adminJox.git
cd adminJox
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Update Configuration
- Copy `appsettings.json` to `appsettings.Development.json`
- Update connection strings and service configurations

### 4. Database Setup
```bash
# Apply Entity Framework migrations
dotnet ef database update --project Infrastructure --startup-project Api
```

### 5. Run the Application
```bash
dotnet run --project Api
```

The API will be available at `https://localhost:5001` or `http://localhost:5000`

## 📖 API Documentation

Once the application is running, you can access:

- **Swagger UI**: `https://localhost:5001/swagger`
- **Health Checks**: `https://localhost:5001/health`
- **API Discovery**: `https://localhost:5001/api/discovery`

## 🐳 Docker Support

### Build Docker Image
```bash
docker build -t adminjox-api .
```

### Run with Docker Compose
```bash
docker-compose up -d
```

## 🧪 Testing

Run unit tests:
```bash
dotnet test
```

## 📁 Project Structure

```
├── Api/                          # Web API layer
│   ├── Controllers/              # API controllers
│   ├── Middleware/               # Custom middleware
│   └── Installers/               # Service configuration
├── Application/                  # Application layer
│   ├── Commands/                 # Command handlers
│   ├── Services/                 # Application services
│   └── Interfaces/               # Service contracts
├── Domain/                       # Domain layer
│   ├── AggregateModels/          # Domain entities
│   ├── Services/                 # Domain services
│   └── Repositories/             # Repository interfaces
├── Infrastructure/               # Infrastructure layer
│   ├── Repositories/             # Repository implementations
│   ├── Cosmos/                   # Cosmos DB integration
│   └── Mongo/                    # MongoDB integration
└── Utilities.Artifacts/          # Shared utilities
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

If you have any questions or need help, please open an issue in the GitHub repository.

---

**Note**: This is a template solution. Make sure to update all configuration files with your actual service endpoints and credentials before deploying to production.
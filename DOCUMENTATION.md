# DVLD System - API

RESTful API system for managing local driving licenses (DVLD) built with ASP.NET Core.

## English Version Coming Soon...

For now, see [Arabic Documentation](./API_DOCUMENTATION_AR.md) for complete API reference.

## Quick Start

1. Clone the repository:
```bash
git clone https://github.com/abdulazizbereket/DVLD-API.git
```

2. Update Connection String in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DVLD;Integrated Security=true;"
}
```

3. Run the application:
```bash
dotnet run
```

4. Access Swagger UI:
```
https://localhost:5000/swagger
```

## Project Structure

```
DVLD-API/
├── Controllers/           # API endpoints
│   ├── PersonController.cs
│   ├── CountryController.cs
│   ├── ApplicationController.cs
│   └── UserController.cs
├── DTOs/                  # Data Transfer Objects
│   ├── PersonDTO.cs
│   ├── CountryDTO.cs
│   ├── ApplicationDTO.cs
│   ├── UserDTO.cs
│   └── ResponseDTO.cs
├── BusinessLayer/         # Business Logic
│   ├── clsPerson.cs
│   ├── clsCountry.cs
│   ├── clsApplication.cs
│   ├── clsUser.cs
│   └── clsValidation.cs
├── DataAccessLayer/       # Database Access
│   ├── clsPersonDataLayer.cs
│   ├── clsCountryDataLayer.cs
│   ├── clsApplicationData.cs
│   ├── clsUserDataLayer.cs
│   └── clsDataAccessLayerSetting.cs
└── Program.cs             # Application startup
```

## Key Endpoints

### Person Management
- `GET /api/person` - Get all persons
- `GET /api/person/{id}` - Get person by ID
- `GET /api/person/national/{nationalNo}` - Get person by national number
- `POST /api/person` - Add new person
- `PUT /api/person/{id}` - Update person
- `DELETE /api/person/{id}` - Delete person

### Country Management
- `GET /api/country` - Get all countries
- `GET /api/country/{id}` - Get country by ID
- `GET /api/country/name/{countryName}` - Get country by name

### Application Management
- `GET /api/application/{id}` - Get application
- `POST /api/application` - Add application
- `PUT /api/application/{id}` - Update application
- `DELETE /api/application/{id}` - Delete application

### User Management
- `GET /api/user/{id}` - Get user by ID
- `GET /api/user/username/{username}` - Get user by username
- `POST /api/user` - Add user
- `PUT /api/user/{id}` - Update user
- `DELETE /api/user/{id}` - Delete user

## Technologies Used

- ASP.NET Core 7.0
- SQL Server
- C# .NET Framework
- Entity Framework Core (planned)
- Swagger/OpenAPI

## Response Format

All responses follow this format:
```json
{
  "success": true/false,
  "message": "Response message",
  "data": {}
}
```

## Requirements

- .NET 7.0 or higher
- SQL Server
- Visual Studio 2022 or VS Code

## Installation

See [Arabic Documentation](./API_DOCUMENTATION_AR.md) for detailed setup instructions.

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Author

Abdulaziz Bereket

## Support

For issues and questions, please open an issue on GitHub.

## Beer Store API

This is a simple ASP.NET Core Web API for managing beers and brands in a store. It provides endpoints for retrieving, adding, updating, and deleting beers and brands.

### Technologies Used

- ASP.NET Core
- Entity Framework Core
- FluentValidation
- Microsoft SQL Server (for data storage)

### Project Structure

- **Backend**: Contains the API code, including controllers, services, models, DTOs, and validators.
- **DTOs**: Data Transfer Objects used for input and output validation.
- **Models**: Entity Framework models representing the database entities.
- **Services**: Business logic and data manipulation services.
- **Validators**: FluentValidation validators for input validation.
- **StoreContext**: Entity Framework DbContext for database interaction.

### API Endpoints

#### Beers

- **GET /api/Beer**: Retrieve a list of all beers.
- **GET /api/Beer/{id}**: Retrieve details of a specific beer by ID.
- **POST /api/Beer**: Add a new beer to the store.
- **PUT /api/Beer/{id}**: Update details of a specific beer by ID.

#### Brands

- **GET /api/Brand**: Retrieve a list of all brands.
- **GET /api/Brand/{id}**: Retrieve details of a specific brand by ID.
- **POST /api/Brand**: Add a new brand to the store.
- **PUT /api/Brand/{id}**: Update details of a specific brand by ID.

### How to Run

1. Clone the repository: `git clone https://github.com/YOUR-USERNAME/beer-store-api.git`
2. Navigate to the project folder: `cd beer-store-api`
3. Open the solution in Visual Studio or your preferred IDE.
4. Set up your SQL Server connection string in the `appsettings.json` file.
5. Run the database migrations to create the required tables: `dotnet ef database update`
6. Run the application: `dotnet run`
7. Access the API at `https://localhost:5001` or `http://localhost:5000`.

### Dependencies

- Microsoft.AspNetCore.App
- Microsoft.EntityFrameworkCore.SqlServer
- FluentValidation
- FluentValidation.AspNetCore

### Contributors

- [Your Name]

Feel free to contribute to the project by adding new features, fixing bugs, or improving documentation. Open issues and pull requests are welcome!
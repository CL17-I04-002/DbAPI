## Project Overview

This project is based on Clean Architecture and built with .NET 8 using the Code First approach with Entity Framework Core.

### Getting Started

To run the project correctly, follow these steps:

1. Update the `appsettings.json` file with your own connection string.
2. Run the initial migration using the following commands in the Package Manager Console:

   ```bash
   Add-Migration InitialCreate
   Update-Database

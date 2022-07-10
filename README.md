# Clean Architecture .NET

This is a Clean Architecture study project using .NET 6.

The project consists of a complete web application for managing products and categories through a website and also through a Rest API, where all data is persisted in a Microsoft SQL Server database.

The MVC site uses authentication by Cookies, and the API uses authentication by Bearer Token JWT, so it is necessary to register and then login to perform the operation and manipulation of data in the system.

To adhere to all Clean Architecture principles, the application was divided into 7 different projects, each one responsible for a part of the application, thus reducing coupling and always directing dependencies from the external layers towards the domain.

These are the projects:

- CleanArcMvc.Domain
- CleanArcMvc.Domain.Tests
- CleanArcMvc.Infra.Data
- CleanArcMvc.Infra.IoC
- CleanArcMvc.Application
- CleanArcMvc.API
- CleanArcMvc.WebUI
#
### Below are some guidelines for running the Application.

<br/>

- Create Microsoft SQL Server Database with Docker
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=@Bruno12345" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

- Install packages for each project
```
dotnet restore
```

- Add packages
```
dotnet add package <Package.Name>
```

- Create migrations
```
dotnet ef --startup-project ../CleanArchMvc.WebUI/ migrations add <MigrationName>
```

- Apply migrations
````
dotnet ef --startup-project ../CleanArchMvc.WebUI/ database update
````

- Run a project in root folder
```
dotnet run --project CleanArchMvc.WebUI/CleanArchMvc.WebUI.csproj
```

- Run for a specific project
```
dotnet run
```

- Run tests in tests project folder
```
dotnet test
```

#

- Default applications users
```
user: user@localhost
pass: @User12345

adm:  admin@localhost
pass: @Admin12345
```

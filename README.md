# My Personal Finances (Angular + ASP .NET Web API)

This project is a Single Page Application built using Angular and ASP.NET Web API. It follows the CQRS + MediatR pattern and clean architecture principles. 

## Usage
This application is a tool for management of personal finances. Users can easy import their income and expenses and use monthly and annual report statistics for optimization of savings and investments. 

## There are six base layers in the project.
1. Domain Layer - contains all entities, enums, exceptions, types and logic specific to the domain. The Entity Framework related classes are abstract, and should be considered in the same light as .NET Core. For testing, use an InMemory provider.
2. Application Layer - contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers.
4. Infrastructure Layer - contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.
3. Persistence Layer - contains database context, all configurations, migrations and data seed. It depends only on the application layer.
4. Presentation Layer - contains all presentation logic. Client side is a Single Page Application based on React working with ASP.NET Web API. There are four razor pages - for login, logout, register and profile. Presentation layer depends only on application layer.
5. Common Layer - contains all cross-cutting concerns.

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2022](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 7.0](https://www.microsoft.com/net/download/dotnet-core/7.0)
* [Node.js](https://nodejs.org/en/) (version 20) with npm (version 9.6.4)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build     
     ``` 
  4. In the `\Presentation\Finance.WebUI` directory, launch the application by running:
     ```
     dotnet run
     ```
  5. Both back-end and front-end will launch in your browser on random port (use back-end port to see application).

## Technologies
* Angular 15
* .NET 7.0
* CQRS, MediatR
  
## License

This project is licensed under the MIT License - see the [LICENSE.md]

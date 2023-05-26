### Hubtel.Wallets

Please find the script containing stored procedures and view and run this in ssms

Admin login:

           email: hubtel.info.gh@gmail.com
           password: Password@2
           UserId: 5ca5f8ba-a92f-e8b2-c666-5667615de41c
           
 
 
 User login:
 
           email: mattoduamafu@gmail.com
           password: Password@1
           UserId: 0be64bd1-d201-7821-9000-18937492a66d
           
    
    
Setup redis running on port 6379 for caching purposes


# WebAPI with ASP.NET Core 3.1

This is a WebAPI project built with ASP.NET Core 3.1, following clean architecture principles, CQRS (Command Query Responsibility Segregation), and the repository pattern. It includes features such as Redis caching, API versioning, authentication, authorization, and API rate limiting.

## Features

- Clean architecture: The project follows a modular and layered architecture to ensure separation of concerns and maintainability.
- CQRS pattern: Command Query Responsibility Segregation (CQRS) is implemented to separate read and write operations, improving performance and scalability.
- Repository pattern: The repository pattern is used to abstract data access and provide a consistent interface for working with data.
- Redis caching: Redis is used for caching frequently accessed data, improving response times and reducing load on the database.
- API versioning: The project includes API versioning to manage and support different versions of the API.
- Authentication and authorization: The project implements authentication and authorization mechanisms to secure access to API endpoints.
- API rate limiting: Rate limiting is implemented to control the number of requests that clients can make to the API within a specific time frame.

## Prerequisites

- .NET Core 3.1 SDK
- Redis server (for caching)

## Getting Started

1. Clone the repository:
   ```shell
   git clone https://github.com/Matthew-Oduamafu/Hubtel.Wallets.git
   
2. Build the solution:
```shell
   dotnet build
```

3. Configure the Redis connection in the appsettings.json file.

4. Run the application:
```shell
   dotnet run
```

5. Access the API using your preferred HTTP client.

<br/>
<br/>
<br/>

![cleanArch](https://github.com/Matthew-Oduamafu/Hubtel.Wallets/assets/72637895/36d5efcd-f392-4cad-9e95-94e3ff5321f3)

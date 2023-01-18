# Architecture
- 2 microservices (catalog, inventory)
- Each service has own db (MongoDB), that is in no relation to other dbs and of only use by that service
- For inter-service communication, system makes use of a message broker (RabbitMQ & MassTransit) 
- For clients, an API gateway which provides flexibility to make changes to the services without impacting the clients
- Run infrastructure (Mongodb, RabbitMQ) via Docker containers

# Installation
- .NET 5: https://dotnet.microsoft.com/en-us/download/dotnet/5.0
- https://docs.docker.com/get-docker/
- https://code.visualstudio.com/download
  - install C# extension on VSC
```
dotnet --info
docker version
```

# Monolith Pros and Cons
A monolithic architecture is a software architecture pattern where an app is composed of a single large codebase, rather than being divided into smaller, independent components. Here are some pros and cons of using a monolithic architecture:

## Pros
- Simplicity: simple to understand, develop, and maintain
- Cohesion: all the code is in a single codebase, it is easy to ensure that the different parts of the app are well integrated and work together seamlessly
- Deployment: deploying is relatively straightforward, as all the code and dependencies are contained in a single package
- Scalability: can be scaled by adding more resources to the server, such as memory or CPU

## Cons
- Complexity: as the app grows, the mono codebase becomes complex
- Coupling: because all the code is in a single codebase, changes in one part of the app can have unintended consequences in other parts of the app
- Deployment: difficult especially if there are many deps
- Scalability: mono apps can be difficult to scale horizontally, as adding more servers can be difficult and costly
- Limited Flexibility: mono apps can be difficult to customize to the specific needs of different clients or users

# Microservices
Microservices is a software architecture pattern where an application is divided into a collection of small, independent services that communicate over a network. Each service is responsible for a specific business function and can be developed, deployed, and scaled independently of the other services.


# Create Services
```
dotnet new webapi -n <name> --framework net5.0
```
- .csproj: project file, defines how project is build and .NET runtime
- Program.cs: entry point of app
- Startup.cs: service registration and request pipeline config
- /Controllers: group of actions that handle requests
- WeatherForecast.cs: model / sample
- launchSettings.json: microservices addr ("applicationUrl")


# Configure Vscode build
- added to tasks.json (configure the VSCode task runner, this means that when you use the command run build task in VSCode it will execute the task that are defined in this group)
```
      "group": {
        "kind": "build",
        "isDefault": true
      }
```

# Build / Run
- build project: 
```
dotnet build
```
- run project:
```
dotnet run
```

# Your connection isnt private
- that's bcs server could not prove that it is localhost; its security certificate is not trusted by computer's OS. This may be caused by a misconfiguration or an attacker intercepting your connection.
- Need to trust cert:
```
dotnet dev-certs https --trust
```

# Easter Egg?
- https://localhost:5001/swagger/ is the WeatherForecast page


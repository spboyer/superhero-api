#Superhero-API

Playground for .NET Core

* Docker
    * Swarm
* .NET Core
* [GenFu](https://github.com/MisterJames/GenFu)
* [Swagger](https://github.com/domaindrivendev/Ahoy)
* nginx


Docker File for ASP.NET Core 1.0.1

```
FROM microsoft/aspnetcore:1.0.1
ARG source=publish
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
COPY $source .
ENTRYPOINT dotnet superhero-api.dll
```

Run command to output the assets for the Docker image
```
dotnet publish -o publish -c release
```

Docker Compose File
```
version: '2'

services:
    superhero-api:
        image: spboyer/superhero-api${TAG}
        build:
            context: .
            dockerfile: Dockerfile
        ports: 
          - "80"
```

Need to run the following command(s)
```
docker-compose up -d
```


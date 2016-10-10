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

Need to run the following command to start/build container 
```

$ docker-compose up -d

# get the port the application is running under on localhost
$ docker ps -a


CONTAINER ID        IMAGE                   COMMAND                  CREATED             STATUS              PORTS                   NAMES
700cbb0b626d        spboyer/superhero-api   "/bin/sh -c 'dotnet s"   45 seconds ago      Up 43 seconds       0.0.0.0:32775->80/tcp   superheroapi_superhero-api_1
```

If there are changes to the application and the images must be rebuilt, remove the image first.

```
$ docker rmi 700c

$ docker-compose up -d  
```

## Docker Swarm

[Details on implementing Swarm](swarm.md) 

## Swashbuckle (Swagger) for API Documentation

[Details on implementing Swashbuckle](swagger.md)
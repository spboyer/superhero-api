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

Initialize the Swarm cluster on your machine
```
docker swarm init
```

Create the service on the swarm cluster.  This initializes a single instance.
```
docker service --publish 5000:80 --name superhero-api spboyer/superhero-api
```

Run `docker service ls` to get the list iof te services running on the cluster
```
ID            NAME          REPLICAS  IMAGE                  COMMAND
dwspfsm279d6  superhero-api  1/1       spboyer/superhero-api
```

Scaling the application on the cluster, use the `scale` command 
```
$ docker service scale superhero-api=3
``

Run `docker ps` to see all of the containers
```
$ docker ps
CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS               NAMES
02164bdc7f2c        spboyer/superhero-api:latest   "/bin/sh -c 'dotnet s"   30 seconds ago      Up 28 seconds       80/tcp              superhero-api.3.eb7em2zuz5z1dxrwbz7utok3l
1885089f097a        spboyer/superhero-api:latest   "/bin/sh -c 'dotnet s"   31 seconds ago      Up 29 seconds       80/tcp              superhero-api.2.7ltz8prph95ofnpms7oogp9n0
8c14ecd0c710        spboyer/superhero-api:latest   "/bin/sh -c 'dotnet s"   2 minutes ago       Up 2 minutes        80/tcp              superhero-api.1.2c6lk2y3qrgd27xjnb489wmpr
```

Swarm has round robin load balancing built in, using `curl` from the bash window each time the `\legion\5` endpoint is hit; the **issuer* relates to the **CONTAINERID** listed above.

```
$ curl http://localhost:5000/api/legions/5
{"guid":"181e74a7-57a8-46d0-8524-5fde4f353ca1","expires":"2016-10-06T20:34:04.042607Z","issuer":"02164bdc7f2c","team":[{"firstName":"Miranda","lastName":"Timms","heroName":"Incredible Shadow"},{"firstName":"Cameron","lastName":"Perez","heroName":"The America"},{"firstName":"Rebecca","lastName":"Young","heroName":"Power Skull"},{"firstName":"Caroline","lastName":"Powell","heroName":"The America"},{"firstName":"Jack","lastName":"Radcliff","heroName":"Dark Doom"}]}shayneboyer @ ~/github/spboyer/superhero-api$ (master)

$ curl http://localhost:5000/api/legions/5
{"guid":"d15b16d6-de09-4cf2-9367-20b806c8139d","expires":"2016-10-06T20:34:12.056385Z","issuer":"1885089f097a","team":[{"firstName":"Catherine","lastName":"Rogers","heroName":"The Doom"},{"firstName":"Vanessa","lastName":"Chambers","heroName":"Metal Hulk"},{"firstName":"Christina","lastName":"Brugger","heroName":"The Knight"},{"firstName":"Brandon","lastName":"Powell","heroName":"Night America"},{"firstName":"Madison","lastName":"Taylor","heroName":"Incredible Shadow"}]}shayneboyer @ ~/github/spboyer/superhero-api$ (master)

```

Scaling down and removing
```
$ docker service scale superhero-api=0

$ docker service rm superhero-api
```

### Swarm notes

Update delay parameter: `update-delay` parameter to configure the swarm with a 5 second update delay, so the new service instances have time to start up.

```
docker service create --publish 5000:80 --name superhero-api --replicas 3 --update-delay 5s spboyer/superhero-api
```

Updating existing or running application after version updates to image

```
docker service update --image spboyer/superhero-api:1.0.4 superhero-api
```


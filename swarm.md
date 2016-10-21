# Swarm creation

Initialize the Swarm cluster on your machine
```
docker swarm init
```

Create the service on the swarm cluster.  This initializes a single instance.
```
docker service --publish 5000:80 --name superhero-api spboyer/superhero-api
```

Run `docker service ls` to get the list of the services running on the cluster
```
ID            NAME          REPLICAS  IMAGE                  COMMAND
dwspfsm279d6  superhero-api  1/1       spboyer/superhero-api
```

Scaling the application on the cluster, use the `scale` command 
```
$ docker service scale superhero-api=3
```

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
{
  "guid": "181e74a7-57a8-46d0-8524-5fde4f353ca1",
  "expires": "2016-10-06T20:34:04.042607Z",
  "issuer": "02164bdc7f2c",
  "team": [
    {
      "firstName": "Miranda",
      "lastName": "Timms",
      "heroName": "Incredible Shadow"
    },
    {
      "firstName": "Cameron",
      "lastName": "Perez",
      "heroName": "The America"
    },
    {
      "firstName": "Rebecca",
      "lastName": "Young",
      "heroName": "Power Skull"
    },
    {
      "firstName": "Caroline",
      "lastName": "Powell",
      "heroName": "The America"
    },
    {
      "firstName": "Jack",
      "lastName": "Radcliff",
      "heroName": "Dark Doom"
    }
  ]
}


$ curl http://localhost:5000/api/legions/5
{
  "guid": "d15b16d6-de09-4cf2-9367-20b806c8139d",
  "expires": "2016-10-06T20:34:12.056385Z",
  "issuer": "1885089f097a",
  "team": [
    {
      "firstName": "Catherine",
      "lastName": "Rogers",
      "heroName": "The Doom"
    },
    {
      "firstName": "Vanessa",
      "lastName": "Chambers",
      "heroName": "Metal Hulk"
    },
    {
      "firstName": "Christina",
      "lastName": "Brugger",
      "heroName": "The Knight"
    },
    {
      "firstName": "Brandon",
      "lastName": "Powell",
      "heroName": "Night America"
    },
    {
      "firstName": "Madison",
      "lastName": "Taylor",
      "heroName": "Incredible Shadow"
    }
  ]
}

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

## Adding nginx 

Create the nginx image
```
docker build -t nginx-hero -f Dockerfile.nginx .
```

Establish the network for the service used for the application
```
docker network create --driver overlay app
```

Start API service on the swarm cluser with three replicas
```
docker service create --name superhero-api --network app --replicas 3 spboyer/superhero-api
```

Start the nginx container on the cluser using the `global` mode, this deploys nginx to each of the API instances
```
docker service create --name nginx-hero --mode=global --network app --publish 5000:80 nginx-hero
```

See the services running on the swarm cluster
```
$ docker service ls
ID            NAME           REPLICAS  IMAGE                  COMMAND
10y9cc0r287v  nginx-hero     global    nginx-hero
9d9j62fkqp9n  superhero-api  1/1       spboyer/superhero-api
```

Browse to http://localhost:5000/api/legions/1. Each time the endpoint is hit, a the `issuer` relates to the **CONTAINER ID** running on the swarm cluster in a round robin load balance method.

```
{
    guid: "7ad4ff43-e006-4c21-9c47-564e4834a0e9",
    expires: "2016-10-07T14:26:00.602494Z",
    issuer: "700a2c3060e3",
    team: [
            {
                firstName: "Claire",
                lastName: "Flores",
                heroName: "The Girl"
            }
          ]
}
```

# Swarm creation

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

Start tne nginx container on the cluser using the `global` mode, this deploys nginx to each of the API instances
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

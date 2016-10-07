# Swarm creation

docker build -t nginx-hero -f Dockerfile.nginx .

docker network create --driver overlay app

docker service create --name superhero-api --network app --replicas 3 spboyer/superhero-api

docker service create --name nginx-hero --mode=global --network app --publish 5000:80 nginx-hero


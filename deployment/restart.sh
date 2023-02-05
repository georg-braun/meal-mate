#! /bin/bash
# This is a very basic script to restart an updated version of the service container

SERVICENAME="meal-mate"

echo "Try to restart container $SERVICENAME"
docker-compose down
docker-compose pull
docker-compose up -d

echo "$SERVICENAME should be up and running :)"
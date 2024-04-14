#!/bin/bash

DOCKER_COMPOSE_FILE="compose.yaml"

if ! command -v docker-compose &> /dev/null; then
    echo "Compose is not found"
    exit 1
fi

if [ ! -f "$DOCKER_COMPOSE_FILE" ]; then
    echo "Not found"
    exit 1
fi

docker-compose up -d
docker-compose ps

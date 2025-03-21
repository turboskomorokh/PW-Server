all: up

build:
	docker build -t pixel-worlds-server -f Dockerfile .

up: build
	docker compose up --no-attach mongodb

.PHONY: version compile build run-tests run-locally run-container-locally

VERSION := $(shell versioner VERSION 2>/dev/null || echo `cat VERSION` -dev)

## Used by Docker / Development

version:
	@echo $(VERSION)
	
tests:
	dotnet build && dotnet test

compile: tests
	@echo "~~~ Compiling project ~~~"
	dotnet clean -c Release && dotnet publish -c Release

build: compile
	@echo "~~~Building image version: $(VERSION) ~~~"
	docker build -t payment-gateway-api .
	
run-locally:
	dotnet build && dotnet run --project src/API/API.csproj
	
run-container-locally: build
	docker run -d payment-gateway-api .
	
## Docker compose development

compose-api-clean:
	docker-compose -f docker-compose.yml down -v --remove-orphans

compose-api: compose-api-clean
	docker-compose -f docker-compose.yml up --force-recreate -d
	
## Docker compose development - prometheus + grafana

compose-with-monitoring-clean:
	docker-compose -f docker-compose-with-monitoring.yml down -v --remove-orphans

compose-with-monitoring: compose-with-monitoring-clean
	docker-compose -f docker-compose-with-monitoring.yml up --force-recreate -d
	
## Performance tests

perf-payments-get:
	k6 run ./performance-tests/payments-get.js --max=1 -i=1 --insecure-skip-tls-verify
	
perf-payments-post:
	k6 run ./performance-tests/payments-post.js --max=1 -i=1 --insecure-skip-tls-verify
	
## Used by CI

build-project:
	dotnet restore && dotnet build

run-tests:
	dotnet test
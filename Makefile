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
	
## Docker compose development - prometheus + grafana + performance tests

build-performance-test-container:
	docker build -t load-testing -f ./performance-tests/docker/Dockerfile ./performance-tests/docker

compose-with-performance-clean:
	docker-compose -f docker-compose-with-performance.yml down -v --remove-orphans

compose-with-performance: compose-with-performance-clean
	docker-compose -f docker-compose-with-performance.yml up --force-recreate -d
	
## Performance tests

perf-payments-get:
	k6 run ./performance-tests/payments-get.js --max=1 -i=1 --insecure-skip-tls-verify
	
perf-payments-post:
	k6 run ./performance-tests/payments-post-201.js --max=10 -i=10000 --insecure-skip-tls-verify
	
## Used by CI

build-project:
	dotnet restore && dotnet build

run-tests:
	dotnet test
	
## Assert my submission using this command
assert-submission: compose-with-performance

assert-submission-clean: compose-with-performance-clean
	  
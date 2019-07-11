.PHONY: version compile build run-tests run-locally run-container-locally

VERSION := $(shell versioner VERSION 2>/dev/null || echo `cat VERSION` -dev)

## Used by Docker / Development

version:
	@echo $(VERSION)

compile:
	@echo "~~~ Compiling project ~~~"
	dotnet clean -c Release && dotnet publish -c Release

build: compile
	@echo "~~~Building image version: $(VERSION) ~~~"
	docker build -t payment-gateway-api .
	
run-locally:
	dotnet run --project src/API/API.csproj
	
run-container-locally: build
	docker run -d payment-gateway-api .
	
## Used by CI

build-project:
	dotnet restore && dotnet build

run-tests:
	dotnet test
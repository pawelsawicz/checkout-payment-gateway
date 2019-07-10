.PHONY: version

VERSION := $(shell versioner VERSION 2>/dev/null || echo `cat VERSION` -dev)
 
version:
	@echo $(VERSION)
	
run-locally:
	dotnet run --project src/API/API.csproj
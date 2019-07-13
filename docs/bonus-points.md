# Comments to the bonus points

## Application logging

I used `Serilog`.

## Application metrics

### What I have done

I used `Prometheus` for collecting the data from the service, 
and `Grafana` for analytics and monitoring.

There is `prometheus-net` library that export basic `HTTP` 
metrics of the service.

### What I would have done

- Add buisness level metrics
- Add alerting and integration with i.e `pagerduty`


## Containerization

I used `docker` to define a container image which contains the service.
I used `docker-compose` for development purpose. 
This configuration contain all nessesary tools to develop and maintain the application. 
User doesn't have to provision anything else apart from having `docker`.

## Authentication

N/A

## API client

N/A

## Build script / CI

I used `Makefile` to define build steps. 
I have integrated repository with `circle ci` that runs CI based on `Makefile`


## Performance testing

I used `k6` for performance testing.

I have created two tests:
   - POST /payments/
   - GET /payments/{id}

## Encryption

N/A

### What I would have done

HTTP message encryption.

## Data storage

I used `EventStore` as it's natural tool for `CQRS/ES`
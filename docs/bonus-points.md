# Comments to the bonus points

## Application logging

I used `Serilog`. It supports structured logging, which was my main reason for chosing this library.

If I would have more time then:

- Introduce Correlation Id. It's very helpful concept for cross referencing 
requests between multiple services

## Application metrics

I used `Prometheus` for collecting the metrics from the service, 
and `Grafana` for analytics and monitoring.

There is `prometheus-net` library that export basic `HTTP` 
metrics of the service.

- Grafana - [http://localhost:3000](http://localhost:3000)
- Prometheus [http://localhost:9090](http://localhost:9090)

If I would have more time then:

- Add business level metrics (i.e number of approved/failed payments)
- Add alerting integration with i.e `pagerduty`

## Containerization

I used `docker` to define a container image which contains the service.
Additionally I introduced `docker-compose` to spin up development environment. 
This configuration contain all necessary tools to develop and maintain the application. 
User does not have to provision anything else apart from having `docker` installed.

## Authentication

I have not completed this.

If I would have more time then:
- Introduce Basic Auth (if it's startup)
- Introduce OAuth (if it's more mature company)

## API client

I have not completed this.

Personally I am against creating `API clients`,
 unless organisation is willing to create separate team (Developers Relationship) 
 which will maintain public documentation and `SDKs`. Maintenance of those is significant.

## Build script / CI

I used `Makefile` to define build steps, then I have integrated repository with `circle ci` 
that runs CI based on the instructions from `Makefile`

If I would have more time then:
- After successful master build, upload crated image to a docker artifactory.
- Deployment script, maybe Kubernetes ?

## Performance testing

I used `k6` for performance testing.

I have created two tests:
   - POST /payments/
   - GET /payments/{id}
   
Once you run them, you can view stats in the console, or just go to grafana - http://localhost:3000


## Encryption

I have not completed this.

I am just going to leave a citation:

> When communicating over the Internet using the HTTP protocol, it can be desirable for a server or client to authenticate the sender of a particular message. It can also be desirable to ensure that the message was not tampered with during transit. This document describes a way for servers and clients to simultaneously add authentication and message integrity to HTTP messages by using a digital signature.

Via: https://tools.ietf.org/id/draft-cavage-http-signatures-08.html

## Data storage

I have not completed this.

Current solution works in-memory, but EventFlow support 
easy integration with major database products: i.e `MSSQL`.

For the initial implementation I would use `MSSQL` or any other `RDBMS`, as it's well known technology. 
If organisation is large and there are problems with `reads`, `writes` (Please see event modeling section),
 then 
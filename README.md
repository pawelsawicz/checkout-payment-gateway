# checkout-payment-gateway

checkout-payment-gateway coding exercise


Extra mile bonus points

- [ ] [Application logging](/docs/bonus-points.md#application-logging)
- [ ] [Application metrics](/docs/bonus-points.md#application-metrics)
- [ ] [Containerization](/docs/bonus-points.md#containerization)
- [ ] [Authentication](/docs/bonus-points.md#authentication)
- [ ] [API client](/docs/bonus-points.md#api-client)
- [ ] [Build script / CI](/docs/bonus-points.md#build-script--ci)
- [ ] [Performance testing](/docs/bonus-points.md#performance-testing)
- [ ] [Encryption](/docs/bonus-points.md#encryption)
- [ ] [Data storage](/docs/bonus-points.md#data-storage)

## TODO business work

1. Merchant should be able to process a payment through the payment gateway 
and receive either a successful or successful response
    1. Simulation of the bank component. Component should be able to be swtiched for 
    a real bank once we move into production
2. A merchant should be able to retrieve the details of a previously made payment

## TODO Platform / Infrastructure work

- [x] Bootstrap projects `api`, `api.tests`
- [ ] Bootstraping API
    - [ ] Add ASP.NET Framework
        - [ ] Drive Kestrel configuration via config file
        - [ ] IoC (?)
    - [x] Add Serilog
        - [x] Add Console sink
    - [x] Add prometheus.net
        - [x] Add basic HTTP metrics
        - [ ] Consider: Add business level metrics
    - [ ] Add Basic authorisation
    - [ ] Enforce HTTPS
- [ ] Add performance testing project
- [ ] Add event store / database (?)
- [ ] Add docker support
    - [x] Add dockerfile
    - [ ] Add docker-compose
        - [x] Create docker-compose just for API
        - [x] Create docker-compose with monitoring tools
    - [ ] Change type of the main from `void` to `int`, 
    better visibility over types of shutdown
- [ ] Add CI configuration (Circle CI)
    - [ ] Create Makefile (build script)
    
### Improvments

- [ ] Add correlation id
- [ ] Add File sink for Serilog
- [ ] Extract test coverage / failure for tests

### Documentation work

- [ ] Create infrastructure diagram
- [ ] Add screenshoot of the CI


### Infrasturcture diagram

![Infrasturcture diagram](/docs/infrastructure-architecture.png)
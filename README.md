# checkout-payment-gateway

## Table of the content

- [Buissnes Deliverables](#buissnes-deliverables)
    - [Event Modeling](/docs/event-modeling.md)
- Commentary to bonus points 
- [Infrastructure Architecture](#infrasturcture-diagram)

## Buissnes Deliverables

1. Merchant should be able to process a payment through the payment gateway 
and receive either a successful or successful response
    1. Simulation of the bank component. Component should be able to be switched for 
    a real bank once we move into production
2. A merchant should be able to retrieve the details of a previously made payment

I modelled two deliverables using event modeling.

### Coding assumtions, and some commentary.

**Main assumption**, I didn't try to reinvent wheel here, I used off the 
shelf solutions whenever possible, this includes:

- CQRS/ES framework
- Validation 

**What I have done**:
- Exploring domain
- Modeling domain via event modeling
- Translate modelled domain into code
- What would be the next step for the platform
- Searching for the pitfalls

Technology stack:
 - API Framework : WebAPI ASP.NET Core
 - CQRS/ES Framework: EventFlow
 - Persistance storage: In-memory
 - aaaaaa

## Bonus points

Extra mile bonus points (please see the [commentary](/docs/bonus-points.md))

- [ ] [Application logging](/docs/bonus-points.md#application-logging)
- [ ] [Application metrics](/docs/bonus-points.md#application-metrics)
- [ ] [Containerization](/docs/bonus-points.md#containerization)
- [ ] [Authentication](/docs/bonus-points.md#authentication)
- [ ] [API client](/docs/bonus-points.md#api-client)
- [ ] [Build script / CI](/docs/bonus-points.md#build-script--ci)
- [ ] [Performance testing](/docs/bonus-points.md#performance-testing)
- [ ] [Encryption](/docs/bonus-points.md#encryption)
- [ ] [Data storage](/docs/bonus-points.md#data-storage)


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
- [x] Add performance testing project
    - [ ] Add performance test for payments - POST
    - [ ] Add performance test for payments - GET
- [ ] Add event store / database (?)
- [ ] Add docker support
    - [x] Add dockerfile
    - [ ] Add docker-compose
        - [x] Create docker-compose just for API
        - [x] Create docker-compose with monitoring tools
          - [x] Link containers together
          - [x] Setup config for `prom` and `grafana`
        - [ ] Create docker-compose with performance tests
            - [ ] Link it together
            - [ ] Mount tests into container & run tests
    - [ ] Change type of the main from `void` to `int`, 
    better visibility over types of shutdown
- [x] Add CI configuration (Circle CI)
    - [x] Create Makefile (build script)
    
### Improvments

- [ ] Add correlation id
- [ ] Add File sink for Serilog
- [ ] Extract test coverage / failure for tests

### Documentation work

- [x] Create infrastructure diagram
- [ ] Add screenshoot of the CI


### Infrasturcture diagram

![Infrasturcture diagram](/docs/infrastructure-architecture.png)
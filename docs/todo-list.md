# TODO list

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
- [x] Add badge for ci

### Coding

- [x] Introduce Payment Aggregate
    - [x] Payment Status
- [x] Introduce Pay Command
    - [x] Acquiring Bank Payment Request
- [x] Introduce PaymentSucceeded Event
    - [x] Payment Status
- [x] Introduce PaymentFailed Event
    - [x] Payment Status
- [x] Introduce Payment Information Read Model
    - [x] Masked card number
    - [x] Card details
        - Without CVV as in [checkout](https://docs.checkout.com/docs/full-card-details-api)
    - [x] Payment details
    - [x] Status code for the payment
    - [x] Links
- [x] Create switch for live Acquiring Bank component
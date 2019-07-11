# checkout-payment-gateway

checkout-payment-gateway coding exercise


Extra mile bonus points

- [ ] Application logging
- [ ] Application metrics
- [ ] Containerization
- [ ] Authentication
- [ ] API client
- [ ] Build script / CI
- [ ] Performance testing
- [ ] Encryption
- [ ] Data storage

## TODO business work

1. Merchant should be able to process a payment through the payment gateway 
and receive either a successful or successful response
    1. Simulation of the bank component. Component should be able to be swtiched for 
    a real bank once we move into production
2. A merchant should be able to retrieve the details of a previously made payment

## TODO Non business work

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
    - [ ] Add dockerfile
    - [ ] Add docker-compose
    - [ ] Change type of the main from `void` to `int`, 
    better visibility over types of shutdown
    
### Improvments

- [ ] Add correlation id
- [ ] Add File sink for Serilog

### Documentation work

- [ ] Create infrastructure diagram
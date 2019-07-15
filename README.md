# checkout-payment-gateway

[![CircleCI](https://circleci.com/gh/pawelsawicz/checkout-payment-gateway.svg?style=svg)](https://circleci.com/gh/pawelsawicz/checkout-payment-gateway)

## Table of the contents

- [Assertion of this submission](#assertion)
- [Business Summary](#business-summary)
    - [Event Modeling](/docs/event-modeling.md)
- [Technical Summary](#technical-summary)
- [Business Deliverables](#business-deliverables)
- [Bonus points](#bonus-points)
    - [Application logging](/docs/bonus-points.md#application-logging)
    - [Application metrics](/docs/bonus-points.md#application-metrics)
    - [Containerization](/docs/bonus-points.md#containerization)
    - [Authentication](/docs/bonus-points.md#authentication)
    - [API client](/docs/bonus-points.md#api-client)
    - [Build script / CI](/docs/bonus-points.md#build-script--ci)
    - [Performance testing](/docs/bonus-points.md#performance-testing)
    - [Encryption](/docs/bonus-points.md#encryption)
    - [Data storage](/docs/bonus-points.md#data-storage)
- [Infrastructure Architecture](#infrasturcture-architecture-commentary)
- [Coding commentary](#coding-commentary)
- [My TODO list](/docs/todo-list.md)

## Business Summary 

Key actors of this service:

1. Merchant
2. Acquiring Bank

This service is responsible for:

1. Reciving a payment from merchant and forwarding the payment to a acquiring bank.
2. Providing information about a payment for a merchant

![service-overview](/docs/service-overview.jpg)

This service is currently in development, therefore acquiring bank has been [mocked out](https://github.com/pawelsawicz/checkout-payment-gateway/tree/master/src/API/Services/FakeAcquiringBankImpls). 
I did not make any assumptions about transportation protocol between this service and a bank. 
There exists an [interface](https://github.com/pawelsawicz/checkout-payment-gateway/blob/master/src/API/Services/IAcquiringBankService.cs), that expose contract between this service and a acquiring bank. 
At this stage of development no further assumptions should be made.

## Technical Summary

**Main assumption**, I didn't try to reinvent wheel while building this project. I used off the 
shelf solutions whenever possible.

Technology stack:
 - API Framework : ASP.NET Core
 - CQRS/ES Framework: EventFlow
 - Persistance storage: In-memory
 - Metrics collector: Prometheus
 - Bunch of libs.
 - Docker
 - There are elements of DDD
 
 ### Assertion
 
 #### What I have done:
 
 - Exploring domain
 - Exploring business requirements
 - Modeling domain via [event modeling](/docs/event-modeling.md)
 - Translate modelled domain into code
 - What would be the next step for the platform
 - Searching for the pitfalls
 - Create infrastructure architecture
 - Writing tests
 
 In order to run and assert my submission:
 
 1. Clone this repository
 2. Make sure you have `docker` installed on your box
 3. Run `make assert-submission`
    1. builds & tests solution
    2. builds docker image
    3. spin up `api`, `prometheus`, `grafana` containers
    4. runs performance tests `post-201`, `post-400` and `get-200`
 4. If you would like to send HTTP request manually, then `make run-locally`. 
 Load `postman` collection from `./postman/Payments.postman_collection.json`
    1. There is just `POST` method, as results of `POST` returns a link to the get method.

Once you run `make assert-submission`. Following names will be aviable for you:

- API - http://localhost:5000/
- Prometheus - http://localhost:9090
- Grafana - http://localhost:3000/

#### Missing pieces

I did not introduce them, to keep this solution reasonable simple and readable.

- I should have introduce Value Objects for `Card Number`

## Business Deliverables

1. A merchant should be able to process a payment through the payment gateway and receive either a
   successful or unsuccessful response
2. A merchant should be able to retrieve the details of a previously made payment

- [x] Build an API that allows a merchant:
    - [x] To process a payment through your payment gateway.
    - [x] To retrieve details of a previously made payment.

- [x] Build a simulator to mock the responses from the bank to test the API from your first deliverable.

## Bonus points

Extra mile bonus points (please see the [commentary](/docs/bonus-points.md))

- [x] [Application logging](/docs/bonus-points.md#application-logging)
- [x] [Application metrics](/docs/bonus-points.md#application-metrics)
- [x] [Containerization](/docs/bonus-points.md#containerization)
- [ ] [Authentication](/docs/bonus-points.md#authentication)
- [ ] [API client](/docs/bonus-points.md#api-client)
- [x] [Build script / CI](/docs/bonus-points.md#build-script--ci)
- [x] [Performance testing](/docs/bonus-points.md#performance-testing)
- [ ] [Encryption](/docs/bonus-points.md#encryption)
- [ ] [Data storage](/docs/bonus-points.md#data-storage)

## Infrasturcture architecture commentary

Some initial ideas about infrastructure architecture. 
We can discuss in depth on F2F stage

### Startup solution

![startup-1](/docs/startup-solution.jpg)

### Mature startup (first problems with scale)

![startup-1](/docs/mature-startup-solution.jpg)

### Mature company (when vertical scaling is not an option)

![startup-1](/docs/mature-company-solution.jpg)
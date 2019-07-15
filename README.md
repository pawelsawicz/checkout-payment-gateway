# checkout-payment-gateway

[![CircleCI](https://circleci.com/gh/pawelsawicz/checkout-payment-gateway.svg?style=svg)](https://circleci.com/gh/pawelsawicz/checkout-payment-gateway)

## Table of the content

- [Assertion of this submission](#assertion)
- [Buissnes Summary](#buissnes-summary)
    - [Event Modeling](/docs/event-modeling.md)
- [Technical Summary](#technical-summary)
- [Buissnes Deliverables](#buissnes-deliverables)
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

## Buissnes Summary 

Key actors of this service:

1. Merchant
2. Acquiring Bank

This service is responsible for:

1. Reciving a payment from merchant's customer and forwarding the payment to a acquiring bank.
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
 
 ### Assertion
 
 #### What I have done:
 
 - Exploring domain
 - Modeling domain via [event modeling](/docs/event-modeling.md)
 - Translate modelled domain into code
 - What would be the next step for the platform
 - Searching for the pitfalls
 - Create infrastructure architecture
 
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

## Buissnes Deliverables

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

![startup-1](/docs/startup-solution.jpg)

![startup-1](/docs/mature-startup-solution.jpg)

![startup-1](/docs/mature-company-solution.jpg)
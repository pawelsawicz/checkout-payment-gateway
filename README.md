# checkout-payment-gateway

[![CircleCI](https://circleci.com/gh/pawelsawicz/checkout-payment-gateway.svg?style=svg)](https://circleci.com/gh/pawelsawicz/checkout-payment-gateway)

## Table of the content

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
 - API Framework : WebAPI ASP.NET Core
 - CQRS/ES Framework: EventFlow
 - Persistance storage: In-memory

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

## Coding commentary

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


## Infrasturcture architecture commentary

![startup-1](/docs/startup-solution.jpg)

![startup-1](/docs/mature-startup-solution.jpg)

![startup-1](/docs/mature-company-solution.jpg)
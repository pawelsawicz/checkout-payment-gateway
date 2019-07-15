# Event Modeling

I used technique called event modeling in order to decompose given problem into:
 `Commands`, `Events` and `Views (ReadModels`).
 
Main benefit of event modeling is that created diagram can be used as 
a specification for the code and tests. As you can see: 
 [here](https://github.com/pawelsawicz/checkout-payment-gateway/blob/master/test/API.Tests/Domain/PayCommandHandlerTests.cs),
 [here](https://github.com/pawelsawicz/checkout-payment-gateway/blob/master/test/API.Tests/Domain/PaymentAggregateTests.cs)
 and [here](https://github.com/pawelsawicz/checkout-payment-gateway/blob/master/test/API.Tests/Domain/PaymentInformationReadModelTests.cs)

Additionally this diagram is a commentary for the code architecture.

## What you need to know ?

- Blue card - Command
- Orange card - Event
- Green card - View (ReadModel)

The idea is that any member of the team should be able to read out all information from the diagram, 
without any additional commentary (assuming that they have taken part in event modeling). 
Otherwise I failed to model the domain :)

## Payment Gateway MVP (Implemented version)

This diagram represents currently implemented version. **This is not a proper CQRS!** 
It's just simple `command` and `event` handling.

Assumptions:
- Customers expect strong consistency (immediate response whether their payment was successful or not).

Benefits:
- Simple system, easy to maintain (code wise)
- Simple state machine

Risks:
- Coupled systems: *payment gateway* and *acquiring bank*, *payment gateway* needs to 
physically awaits for the callback from the *acquiring bank*.

Situations to consider, when this architecture could be implemented:
- Early stage company
- Read/Write is not a problem
- Vertical scaling is still an option 

![event-modeling-mvp](payment-gateway-features-1.jpg)


## Payment Gateway (CQRS version)

This diagram represents version if we would like to achieve decoupling and support for heavy writes.
**Assuming** that initial version was implemented as proposed above, this update (coding-wise) is cheap.

Assumptions:
- Merchants are fine with eventual consistency

Benefits:
- Decoupled systems: *payment gateway* and *acquiring bank*. Intention to pay from the actual forwarding 
request to   *acquiring bank*.
- Our system does not depend on *acquiring bank* system, we can stay operational 
and collect requests even if *acquiring bank* is offline.
- Better write throughput, as system does not await for callback from an *acquiring bank* system

Risks:
- Introduce additional complexity to the business
    i.e: what if payment was not authorized despite that customer got the bought item
- It may introduce bad user experience both for customers and merchants
- Increased number of calls to customer service due to changed behaviour

Situations to consider, when this architecture could be helpful:
- Huge sales events i.e black friday

![event-modeling-mvp](payment-gateway-features-2.jpg)
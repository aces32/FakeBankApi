# Fake Bank API

An internal API for a fake financial institution that enables transfers and account creation

## Architecture Used

Clean Architecture (.Net core 5.0).

```python
Code Structure

#Src
  - #API
     - FakeBank.BankAPI.Api (API Endpoint)
  - #Core
     - FakeBank.BankAPI.Application (Application Logic - implemented in the features folder) 
     - FakeBank.BankAPI.Domain (Database Model)
  - #Infrastructure
     - FakeBank.BankAPI.Identity (Auth0 Authentication Implementation)
     - FakeBank.BankAPI.Infrastructure (Logging, Mail e.t.c...)
     - FakeBank.BankAPI.Persitence (Database Repository, dbcontext and logic)
#test
     - FakeBank.BankAPI.IntegrationTests
     - FakeBank.BankAPI.UnitTests

```

## Technologies Used

```python
# FakeBank.BankAPI.API
- Swagger for API documentation
- Mediatr - sends a request to the application logic via Irequest Handler

# FakeBank.BankAPI.Application
- Automapper
- FluentValidation - Api requests validations
- Mediatr - Receives requests from the API and carries out appropriate implementation

# FakeBank.BankAPI.Domain
- Ef core

# FakeBank.BankAPI.Identity
- Auth0
- Jwt

# FakeBank.BankAPI.Infrastructure
- SendGrid
- Serilog for logging

# FakeBank.BankAPI.Persitence
- Ef Core

# FakeBank.BankAPI.IntegrationTests
- xunit
- Ef Core inmemory

# FakeBank.BankAPI.UnitTests
- Xunit
- Moq
- Shouldly
``` 

## Auth0 Token Generation

```python
# API URL 
https://dev-p-zm5f3q.us.auth0.com/oauth/token

# Payload
 { 
   "grant_type": "client_credentials", 
   "client_id": "tGgB4XhOVzTIESMteqrNCTXKAYpTkIhC" ,
   "client_secret": "81xJ2XgiM3YG1BQVR-_ZaFCk2Y4bAZ8uU0v73xJBgMIzAR3S4ASikAaY-TIQ5L9z",
   "audience": "https://dev-p-zm5f3q.us.auth0.com/api/v2/"
 }  

pass the access token returned from the API as bearer token
```

## Notes
Ensure that the Swagger XML File is present in the specified bin folder the app is running (i.e).
```python
# FakeBank.BankAPI.API should have a 
- FakeBank.BankAPI.API.XML file in the bin\Debug\net5.0 folder

# FakeBank.BankAPI.Application should have a 
- FakeBank.BankAPI.Application .XML file in the bin\Debug\net5.0 folder 

This XML files where generated to aid swagger documentation and better visualization of what the API does
```

### Objective

Your assignment is to build an internal API for a fake financial institution using C# and any framework.

### Brief

While modern banks have evolved to serve a plethora of functions, at their core, banks must provide certain basic features. Today, your task is to build the basic HTTP API for one of those banks! Imagine you are designing a backend API for bank employees. It could ultimately be consumed by multiple frontends (web, iOS, Android etc).

### Tasks

- Implement assignment using:
  - Language: **C#**
  - Framework: **any framework**
- There should be API routes that allow them to:
  - Create a new bank account for a customer, with an initial deposit amount. A
    single customer may have multiple bank accounts.
  - Transfer amounts between any two accounts, including those owned by
    different customers.
  - Retrieve balances for a given account.
  - Retrieve transfer history for a given account.
- Write tests for your business logic

Feel free to pre-populate your customers with the following:

```json
[
  {
    "id": 1,
    "name": "Arisha Barron"
  },
  {
    "id": 2,
    "name": "Branden Gibson"
  },
  {
    "id": 3,
    "name": "Rhonda Church"
  },
  {
    "id": 4,
    "name": "Georgina Hazel"
  }
]
```

You are expected to design any other required models and routes for your API.

### Evaluation Criteria

- **C#** best practices
- Completeness: did you complete the features?
- Correctness: does the functionality act in sensible, thought-out ways?
- Maintainability: is it written in a clean, maintainable way?
- Testing: is the system adequately tested?
- Documentation: is the API well-documented?

### CodeSubmit

Please organize, design, test and document your code as if it were going into production - then push your changes to the master branch. After you have pushed your code, you may submit the assignment on the assignment page.

All the best and happy coding,

The SaltPay Team
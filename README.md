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


# Sample .NET 5 Custom Authentication Scheme
This repo is an [opinionated] happy-path implementation of a custom cookie-based authentication scheme in .NET 5.

**CAVEAT:** This is happy-path only and should not be considered production-grade.

## Contrived Problem Statement/Scenario
Allow seemless cookie-based authentication across multiple services in a scenario whereby:
1. The primary application is a ServiceStack implementation (either .NET 5 or .NET Framework)
2. The primary application currently uses credential authentication, with cookie-based sessions stored in Redis
3. Secondary API service(s) exist and are implemented in .NET 5
4. For the purposes of the scenario, other schemes (e.g bearer tokens) are deemed out of scope

## Design Decisions
* Primary app and secondary API(s) are same-domain
* Secondary APIs only require per-request authentication/authorization, and
* No session management (sign-in/sign-out) functionaltiy is required on the secondary API(s)
* A minimal level of coupling (Redis instance) is acceptable


## Steps ##
1. Start Redis:
```
shell# docker-compose up
```

2. Start the ServiceStack App
```
shell# dotnet run --project src/ServiceStackWeb/WebApp
```

3. Start the .NET 5 API
```
shell# dotnet run --project src/Jt.CustomAuthScheme.Api
```

4. Attempt to access a protected resource `https://localhost:5051/weatherforecast`.
You should expect an Unauthorised (401) response.

5. Log in to the ServiceStack app `https://localhost:5001/auth` with details:
* Username: `admin@email.com`
* Password: `p@55wOrd`

6. Now re-attempt to access the protected resource: `https://localhost:5051/weatherforecast`
You should expect to see JSON data.

7. Access data of the request's user: `https://localhost:5051/user`
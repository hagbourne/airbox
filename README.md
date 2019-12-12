# Airbox.Api
Airbox technical test.

## Overview
This is a sample API developed as a coding test. The full brief is as follows:

Create a web service which exposes a RESTful API for the storage and retrieval of the location history (specified as latitude and longitude) for a set of users.

The API should be able to:
1. Receive a location update for a user
2. Return the current location for a specified user
3. Return the location history for a specified user
4. Return the current location for all users
5. Return the current location for all users within a specified area

The application should provide a suitable foundation for other developers to enhance and
improve upon in the near and distant future.

## Details
The API supports a single endpoint, `/api/v1/users/{username}/locations` with an optional parameter `?current=1`.

OpenAPI documentation, provided using Swashbuckle/Swagger, can be found at the endpoint `/swagger/`.

The development was completed using .NET Core 2.1 as this is the current LTS version. 

The architecture of the service is derived from recent work of mine.
* The controller redirects processing to operations provided by a shared service. It also handles mapping of the model data object to resource representations.
* Mapping is handled by a combination of LINQ and Automapper to "unflatten" rows into a hierarchical structure (and the reverse of this).
* The service in this case does little more than passthrough the service operations to repository actions but provides the location for further processing/validation.
* The repository here is an in-memory repository as a simple `List<>` but in practice would be implemented over a persistent store.

A dockerfile is present which will configure building of a container ready for hosting. For this example I have hosted as a "Container Instance" in Azure.
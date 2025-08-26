# Wednesday Morning Lab

We need another service for our Issue Tracker app. This service will be an HTTP API.

## Purpose

This API will "own" the Help Desk Employee VIPs.

### What is a "VIP"?

These are employees that, for reasons determined by the manager of the Help Desk, deserve priority service when reporting a problem to the help desk.

### Functionality Required

#### Adding a VIP

The user should be able to POST a request to the VIP endpoint.

Example:

```http
POST http://localhost:1338/vips
Content-Type: application/json

{
    "sub": "sue@company.com",
    "description": "Sue is the CEO, We need to make sure she is always able to be effective"
}
```

Properties:

- `sub` This is the `sub`ject claim from the identity token. 
    - Required
    - Must be unique. You can't add the same VIP twice. If the same `sub` is posted more than once, return an Http Status Code of 409 - Conflict.
- `description` - a brief description of why they are considered a VIP. 
    - Required
    - Should be at least 10 characters and less than 500 characters.

A successful POST should return:

```http
201 Created
Location: /vips/{id}
Content-Type: application/json

{
    "id": "Id as Guid",
    "sub": "sue@company.com",
    "description": "description from request",
    "addedOn": "ISO 8601 of the date this was added"
}
```

### Getting a VIP

Implement the `GET /vips/{id}` endpoint.

### Getting All Vips

Implement `GET /vips` endpoint.

This should return a list of each of the VIPs.

### Removing a VIP

Implement a `DELETE /vips/{id}` endpoint.

This should always return a `204 No Content` response.

- Rules:
    - And existing VIP that is removed (deleted) should *never* be removed from the database.
    - If the VIP does exist, simply mark it somehow as "removed". 
        - Note, this means the POST above may actually "reactivate" a "removed" VIP.

## Bonus (If Time Allows) Steps

- Implement Open Telemetry For this API. (Note, the Nuget packages have already been added to the starter project)
- Create a custom Otel meter of a counter for each time a VIP is added or removed.
- 
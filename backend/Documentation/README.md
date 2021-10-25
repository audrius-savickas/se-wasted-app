# Documentation

## Solution structure
It is divided in the following projects:
* Contracts:
    - **Goal:** Define the objects that are transferred from the backend to the frontend and viceversa.
    - **Structure:** 
        - DTO ( definition of data transfer objects )
* Domain:
    - **Goal:** Define the objects used on the application and functionality to read and modify them.
    - **Structure:**
        - Comparers: allow to sort list of entities.
        - Entities: definition of all the objects used on the app.
        - Helpers: encompass functionality to be used in other objects.
* Persistence:
    - **Goal:** Data management.
    - **Structure:**
        - DB
        - Interfaces: definition of the avalaible functions.
        - Repositories: implementation of the interfaces.
* Services:
    - **Goal:** It adds validations and logic. It calls the respective repositories. 
    - **Structure:**
        - Interfaces: definition of the avalaible functions.
        - Repositories: implementation of the interfaces.
* WebApi:
    - **Goal:** API. It allows the communication between the frontend and the backend.
    - **Structure:**
        - Controllers: Definition of all the endpoints from the app.
        - Properties: Settings.

## Database
* [Check DB documentation](./DB)
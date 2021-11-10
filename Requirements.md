## Requirements

1. Lazy initialization.
   - DBConfigurations.cs line: 10.  
2. Generics (in delegates, events and methods)
    - All ```Base``` classes and interfaces. 
3. Delegates.
   - RestaurantService.cs line: 99
   - FoodService.cs line: 95
4. Events and their usage: standard and custom.
   - ```RestaurantService``` publishes ```RestaurantRegistered``` event. ```EmailService``` subscribes to this event. When event is raised, email about successful registration is sent to restaurant's email adress.
5. Exceptions and dealing with them in a meaningfull way.
   - todo???
6. Anonymous methods.
   - todo
7. Lambda expressions.
   - EntitySorter.cs
8. Concurrent programming (threading or async/await (for your own written classes); common resource usage between threads).
   - EmailService.cs sending emails asyncronously.
   - todo more???
9.  Web service implemented and used
    - WebApi project, swagger.
10. Dependency Injection.
    - Startup.cs injecting services.

   
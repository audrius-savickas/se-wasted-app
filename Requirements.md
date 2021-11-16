## Requirements

1. Lazy initialization.
   - DBConfigurations.cs line: 10.  
     - We create a thread-safe singleton of DBConfiguration.
     - See [this](https://stackoverflow.com/a/6847761) and [this](https://stackoverflow.com/a/6847882) for a clear explanation why it's used.
2. Generics (in delegates, events and methods)
    - All ```Base``` classes and interfaces. 
      - We have a ```BaseEntity``` class that we use in a generic interface ```IBaseRepository<T>```. We extend other interfaces to work with entities from this generic interface. This way we only need to implement ```BaseRepository``` for basic data access and manipulation from files.
3. Delegates.
   - RestaurantService.cs line: 99
     - ```Register``` method takes a standard generic delegate ```Func<string>``` for id generation.
   - FoodService.cs line: 95
     - ```RegisterFood``` method takes a standard generic delegate ```Func<string>``` for id generation.
   - See more about standard delegate types
4. Events and their usage: standard and custom.
   - ```RestaurantService``` publishes ```RestaurantRegistered``` event. ```EmailService``` subscribes to this event. When event is raised, email about successful registration is sent to restaurant's email adress.
     - [Examples](https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-raise-and-consume-events). See example 3 to better understand how events are defined.
5. Exceptions and dealing with them in a meaningfull way.
   - Custom exceptions. Benefits of custom C# Exception types:
     - Calling code can do custom handling of the custom exception type
     - Ability to do custom monitoring around that custom exception type
6. Anonymous methods.
   - EmailService.cs line: 15-19.
     - Anonymous methods provide a technique to pass a code block as a delegate parameter. Anonymous methods are the methods without a name, just the body.
     - [Example](https://www.tutorialspoint.com/csharp/csharp_anonymous_methods.htm).
7. Lambda expressions.
   - EntitySorter.cs
     - You use a lambda expression to create an anonymous function.
     - Any lambda expression can be converted to a delegate type. The delegate type to which a lambda expression can be converted is defined by the types of its parameters and return value. If a lambda expression doesn't return a value, it can be converted to one of the ```Action``` delegate types; otherwise, it can be converted to one of the ```Func``` delegate types.
     - Google about Func and Action delegates, multicasting delegates.
8. Concurrent programming (threading or async/await (for your own written classes); common resource usage between threads).
   - EmailService.cs sending emails asyncronously.
9.  Web service implemented and used
    - WebApi project, swagger.
10. Dependency Injection.
    - Startup.cs injecting services.

   
# Messaging between microservices

Microservices use messages to communicate between each other. The MessageBase class has an identifier and a creation date. There are two different type of messages that inherit the MessageBase:

- **IntegrationCommandBase**: These kind of messages can be consumed by one consumer. If more than one consumer is subscribed to a message that inherits this base, then an exception will be thrown.
- **IntegrationEventBase**: There is no limitation how many consumer can consume this kind of message.

### Creating a message using one of the base classes:

Create a normal class with the desired name and parameters, then inherit the correct message base class. The class has to be in the Interface assembly of the project within the Events or Commands folder.

Example:

_Location_: MyMicroservice.Interface\Events

```cs
public class MyEvent : IntegrationEventBase
{
    // properties
}
```

```cs
public class MyCommand : IntegrationCommandBase
{
    // properties
}
```

### Creating a consumer for a message:

A consumer must implement the `IConsumer<TMessage>` interface.
The consumer class file should be in the Application assembly of the microservice.

Example:

```cs
public class MyEventConsumer : IConsumer<MyEvent>
{
    // ...

    public PackageCreatedEventConsumer(/* services from DI */)
    {
       // ...
    }

    public async Task Consume(ConsumeContext<MyEvent> context)
    {
        // Do stuff
    }
}
```

### Setting up RabbitMq in a microservice:

First of all you have to add a project reference to:

```
@\libs\CSONGE.Messaging\CSONGE.Messaging.csproj
```

If you have a reference to the project above, then in the Startup class register the RabbitMq service within the `ConfigureServices` using `AddRabbitMq(...)` method provided by the messaging library.

This method has 4 parameter which are the following:

- **rabbitMqConfigSection**: IConfigurationSection for RabbitMq configuration
  - HostName, Port, Virtual Host, Username, Password
- **messagingConfigSection**: IConfigurationSection for message retry
  - RetryCount, MinInterval, MaxInterval, IntervalDelay
- **sentCommandsAssemblies**: Assembly array, these Assemblies contain Commands that inherit IntegrationCommandBase, so they have to be registered as commands to be separated from events.
- **consumerAssemblies**: Assembly array, Assemblies that contain message consumers for the current microservice.

Example for adding RabbitMq to a microservice:

```cs
services
    .AddRabbitMq(
        rabbitMqConfigSection: Configuration.GetSection("RabbitMQ"),
        messagingConfigSection: Configuration.GetSection("Messaging"),
        sentCommandsAssemblies: new[]
        {
            Assembly.Load("Status.Interface"),
        },
        consumerAssemblies: new[]
        {
            Assembly.Load("Partner.Application")
        }
    );
```

In the appsettings.json file specify the RabbitMq and Messaging variables configuration - example:

```json
"RabbitMQ": {
    "HostName": "localhost",
    "Port": 5672,
    "QueueName": "Partner",
    "Username": "guest",
    "Password": "guest"
  },
  "Messaging": {
    "ConsumerErrorHandling": {
      "RetryCount": 3,
      "MinInterval": "00:00:00.5000000",
      "MaxInterval": "00:00:10.0000000",
      "ExponentialBaseIntervalDelta": "00:00:00.5000000"
    }
  }
```

### RabbitMq container

If you are using docker-compose, you just have to add a container in your docker-compose file under the services section:

```yml
rabbitmq:
  image: rabbitmq:3-management-alpine
```

In the docker-compose.override file under the services section you can configure your RabbitMq container, for example:

```yml
rabbitmq:
  container_name: rabbitmq
  restart: always
  ports:
    - "5672:5672"
    - "15672:15672"
```

If you are using your microservices in containers, you have to set some of the RabbitMq configuration variables in the override file - example:

```yml
yourContainer:
  # ...
  environment:
    -  # ...
    - RabbitMQ__HostName=rabbitmq
    - RabbitMQ__Port=5672
```

# AutotaskNet

This library assists in reading and writing from the [Autotask REST API](https://autotask.net/help/developerhelp/Content/APIs/REST/REST_API_Home.htm).

This library is currently in beta and is not recommended for production environments.

## Getting Started

Add a reference to the [NuGet package](https://www.nuget.org/packages/AutotaskNet) in your application:
```pwsh
Install-Package AutotaskNet
```

```bash
dotnet add package AutotaskNet
```

Register the library in the dependency injection container:

```c#
using AutotaskNet.Api;

builder.Services.AddAutotask(new AutotaskCredentials
{
    ApiIntegrationCode = "asdf",
    UserName = "asdf",
    Secret = "asdf"
});
```

If you have not yet set up your Autotask API User, refer to their [documentation](https://autotask.net/help/DeveloperHelp/Content/APIs/REST/General_Topics/REST_Security_Auth.htm).

Finally, inject the `IAutotaskService` to your class.

## Usage Examples

### Creating Entities

```c#
IAutotaskService service;

// Root entity
var ticket = new Ticket { ... };
await service.CreateAsync(ticket);

// Child entity
var ticketNote = new TicketNote { ... };
await service.CreateAsync(ticket.Id, ticketNote);
```

### Querying Entities

```c#
// Root entity
Ticket ticket = await service.GetAsync<Ticket>(12345);
IEnumerable<Ticket> tickets = await service.QueryAsync<Ticket>(QueryFilter.All, 5); // 5 pages of 500 entities
int ticketUdfMatches = await service.QueryCountAsync<Ticket>(new QueryFilter
{
    Filters =
    [
        new QueryFilter.Filter
        {
            Operator = Operator.Equal,
            Field = "MyUdf",
            Value = "Value",
            IsUdf = true
        }
    ]
});

// Child entity
TicketNote ticketNotes = await service.QueryAsync<TicketNote>(ticket.Id);
TicketNote ticketNote = await service.GetAsync<TicketNote>(ticket.Id, 12345);
```

### Updating Entities

```c#
// Root entity
Ticket ticket = await service.GetAsync<Ticket>(12345);
ticket.Title = "New title";
await service.UpdateAsync(ticket);

// Child entity
TicketNote ticketNote = await service.GetAsync<TicketNote>(ticket.Id, 12435);
ticketNote.Description = "New description";
await service.UpdateAsync(ticket.Id, ticketNote);
```

### Deleting Entities

```c#
// Root entity
Ticket ticket = await service.GetAsync<Ticket>(12345);
await service.DeleteAsync<Ticket>(ticket.Id);

// Child entity
TicketNote ticketNote = await service.GetAsync<TicketNote>(ticket.Id, 12435);
await service.DeleteAsync<TicketNote>(ticket.Id, ticketNote.Id);
```

### Retrieving Entity Metadata

```c#
// Root entity
EntityInformationResult entityInformation = await service.GetEntityInformationAsync<Ticket>();
EntityFieldsResult entityFields = await service.GetEntityFieldsAsync<Ticket>();
EntityUserDefinedFieldsResult entityUserDefinedFields = await service.GetEntityUserDefinedFieldsAsync<Ticket>();

// Child entity
EntityInformationResult entityInformation = await service.GetEntityInformationAsync<TicketNote>(ticket.Id);
EntityFieldsResult entityFields = await service.GetEntityFieldsAsync<TicketNote>(ticket.Id);
EntityUserDefinedFieldsResult entityUserDefinedFields = await service.GetEntityUserDefinedFieldsAsync<TicketNote>(ticket.Id);
```

# SmartHome API

Moderní GraphQL API pro správu inteligentní domácnosti s možností spravovat domácnosti, místnosti a zařízení.

## O projektu

SmartHome API je backendová aplikace postavená na .NET s GraphQL rozhraním. Umožňuje uživatelům spravovat své domácnosti, místnosti v domácnostech a zařízení v místnostech prostřednictvím jednotného API.

## Architektura

Projekt je strukturován podle principů **Clean Architecture** s vrstvami:

- **API Layer** (`SmartHome.Api`) - GraphQL endpoint, mappers a dependency injection
- **Application Layer** (`SmartHome.Application`) - Business logic, command/query handlers a MediatR pipeline
- **Domain Layer** (`SmartHome.Domain`) - Domain entities a enums
- **Infrastructure Layer** (`SmartHome.Infrastructure`) - Database context, repositories a entity configurations
- **Test Layer** (`SmartHome.Test`) - Unit testy s NSubstitute a FluentAssertions

## Hlavní služby

### Domácnosti (Homes)

Správa domácností v systému.

#### Operace:
- **CreateHome** - Vytvoření nové domácnosti s adresou
- **GetHomeById** - Získání detailů domácnosti podle ID
- **GetAllHomes** - Získání seznamu všech domácností
- **UpdateHome** - Aktualizace informací o domácnosti (jméno, adresa)
- **RemoveHome** - Odstranění domácnosti (pouze pokud nemá místnosti)

#### Příklady GraphQL:

```graphql
mutation {
  createHome(request: {
    name: "Můj dům"
    street: "Hlavní ulice 123"
    city: "Praha"
    zipCode: "11000"
  })
}

query {
  allHome {
    id
    name
    street
    city
  }
}

query {
  homeById(id: "81cf53b2-4f40-48c3-9225-f3d435003a44") {
    ... on HomeTypeResponse {
      id
      name
      street
      city
      zipCode
    }
    ... on GetErrorResult {
      message
    }
  }
}

mutation {
  updateHome(homeId: "81cf53b2-4f40-48c3-9225-f3d435003a44", request: {
    name: "Aktualizovaný dům"
    city: "Brno"
  })
}

mutation {
  removeHomeById(homeId: "81cf53b2-4f40-48c3-9225-f3d435003a44")
}
```

### Místnosti (Rooms)

Správa místností v domácnostech.

#### Operace:
- **CreateRoom** - Vytvoření nové místnosti v domácnosti
- **GetRoomById** - Získání detailů místnosti podle ID
- **GetRoomsByHomeId** - Získání seznamu místností v domácnosti
- **UpdateRoom** - Aktualizace informací o místnosti (jméno, typ)
- **RemoveRoom** - Odstranění místnosti (pouze pokud nemá zařízení)

#### Typy místností:
- `Unknown` - Neznámá
- `LivingRoom` - Obývací pokoj
- `Kitchen` - Kuchyň
- `Bedroom` - Ložnice
- `Bathroom` - Koupelna
- `Garage` - Garáž

#### Příklady GraphQL:

```graphql
mutation {
  createRoom(request: {
    homeId: "81cf53b2-4f40-48c3-9225-f3d435003a44"
    name: "Obývací pokoj"
    type: LivingRoom
  }) {
    ... on CreateRoomSuccess { 
      id 
    }
    ... on GetErrorResult { 
      message 
    }
  }
}

query {
  roomsByHomeId(homeId: "81cf53b2-4f40-48c3-9225-f3d435003a44") {
    ... on RoomsListResponse {
      rooms {
        id
        name
        type
      }
    }
  }
}

query {
  roomById(id: "00a298f8-d702-4040-b51d-10437c355f53") {
    ... on RoomTypeResponse {
      id
      name
      type
    }
    ... on GetErrorResult {
      message
    }
  }
}

mutation {
  updateRoom(roomId: "00a298f8-d702-4040-b51d-10437c355f53", request: {
    name: "Koupelna"
    type: Bathroom
  })
}

mutation {
  removeRoomById(roomId: "e71db8b9-4859-47b7-893d-5f62728ac1dd")
}
```

### Zařízení (Devices)

Správa chytrých zařízení v místnostech.

#### Operace:
- **CreateDevice** - Vytvoření nového zařízení v místnosti
- **GetDeviceById** - Získání detailů zařízení podle ID
- **GetDevicesByRoomId** - Získání seznamu zařízení v místnosti
- **UpdateDevice** - Aktualizace informací o zařízení (jméno, model, stav)
- **RemoveDevice** - Odstranění zařízení

#### Stavy zařízení:
- `Offline` - Offline
- `Online` - Online
- `Error` - Chyba

#### Příklady GraphQL:

```graphql
mutation {
  createDevice(request: {
    roomId: "00a298f8-d702-4040-b51d-10437c355f53"
    name: "Stropní světlo"
    model: "SL-1000"
    manufacturer: "ACME Lighting"
    state: Offline
  }) {
    ... on CreateDeviceSuccess {
      id
    }
    ... on GetErrorResult {
      message
    }
  }
}

query {
  devicesByRoomId(roomId: "00a298f8-d702-4040-b51d-10437c355f53") {
    ... on DevicesListResponse {
      devices {
        id
        name
        model
        manufacturer
        state
      }
    }
    ... on GetErrorResult {
      message
    }
  }
}

query {
  deviceById(deviceId: "c80c7fc8-db93-4ce9-9f37-74375e627a95") {
    ... on DeviceTypeResponse {
      id
      name
      model
      manufacturer
      state
    }
    ... on GetErrorResult {
      message
    }
  }
}

mutation {
  updateDevice(deviceId: "c80c7fc8-db93-4ce9-9f37-74375e627a95", request: {
    name: "Aktualizované světlo"
    state: Online
  })
}

mutation {
  removeDeviceById(deviceId: "c80c7fc8-db93-4ce9-9f37-74375e627a95")
}
```

## Vrstva aplikace (Application Layer)

### Příkazy (Commands) a dotazy (Queries)

Příkazy a dotazy jsou implementovány podle **CQRS (Command Query Responsibility Segregation)** patternu a využívají MediatR:

- `CreateHomeCommand` - Vytvoří novou domácnost
- `UpdateHomeCommand` - Aktualizuje domácnost
- `RemoveHomeCommand` - Odstraní domácnost
- `CreateRoomCommand` - Vytvoří novou místnost
- `UpdateRoomCommand` - Aktualizuje místnost
- `RemoveRoomCommand` - Odstraní místnost
- `CreateDeviceCommand` - Vytvoří nové zařízení
- `UpdateDeviceCommand` - Aktualizuje zařízení
- `RemoveDeviceCommand` - Odstraní zařízení

Dotazy vrací data ze systému bez změny stavu:

- `GetHomeByIdQuery` - Vrátí domácnost podle ID
- `GetAllHomesQuery` - Vrátí všechny domácnosti
- `GetRoomByIdQuery` - Vrátí místnost podle ID
- `GetRoomsByHomeIdQuery` - Vrátí místnosti domácnosti
- `GetDeviceByIdQuery` - Vrátí zařízení podle ID
- `GetDevicesByRoomIdQuery` - Vrátí zařízení místnosti

### Výsledky operací

- `DeleteResultStatus` - Výsledek mazání (Deleted, NotFound, HasRelatedRecords, Error)
- `UpdateResultStatus` - Výsledek aktualizace (Success, NotFound, ValidationError, Error)

## Infrastruktura

### Database

Aplikace používá **Entity Framework Core** s databází připravené v `ApplicationDbContext`.

### Repositories

Rozhraní pro přístup k datům jsou implementována podle **Repository patternu**:

- `IHomeRepository` - Operace s domácnostmi
- `IRoomRepository` - Operace s místnostmi
- `IDeviceRepository` - Operace se zařízeními

## Testování

Projekt obsahuje jednotkové testy pro všechny business logic handlry:

### Coverage:
- ✅ CreateHome, UpdateHome, RemoveHome, GetHomeById, GetAllHomes
- ✅ CreateRoom, UpdateRoom, RemoveRoom, GetRoomById, GetRoomsByHomeId
- ✅ CreateDevice, UpdateDevice, RemoveDevice, GetDeviceById, GetDevicesByRoomId

### Spuštění testů:

```bash
dotnet test SmartHome.Test
```

### Test Framework:
- **NSubstitute** - Mocking
- **FluentAssertions** - Fluent assertion API
- **xUnit** - Test framework

## Konfigurace

### appsettings.json

Konfigurační soubor pro databázi a logging. Pro vývoj vytvoř `appsettings.json` na základě `appsettings.Template.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Database": {
    "Server": "localhost",
    "Port": 5432,
    "User": "YOUR_DB_USER",
    "Password": "YOUR_DB_PASSWORD",
    "DatabaseName": "SmartHome"
  },
  "AllowedHosts": "*"
}
```

**Důležité:** 
- Soubor `appsettings.Template.json` je šablona pro nastavení
- Zkopíruj jej na `appsettings.json` a vyplň svoje údaje
- **Nikdy necommituj `appsettings.json` s reálnými hesly!** Je v `.gitignore`
- Každý vývojář si musí nastavit své vlastní databázové údaje podle svého prostředí

## Spuštění aplikace

### Požadavky:
- .NET 8 nebo novější
- PostgreSQL 12+

### Příkazy:

```bash
# Obnovení závislostí
dotnet restore

# Spuštění migrace databáze
dotnet ef database update --project SmartHome.Infrastructure

# Spuštění aplikace
dotnet run --project SmartHome.Api

# Spuštění testů
dotnet test

# Build projektu
dotnet build
```

Aplikace bude dostupná na `http://localhost:5000/graphql`

## Struktura složek

```
SmartHome/
├── SmartHome.Api/              # GraphQL API endpoint
│   ├── GraphQL/                # GraphQL queries, mutations, types
│   ├── Mappers/                # DTO mappers
│   └── Program.cs              # Application setup
├── SmartHome.Application/      # Business logic layer
│   ├── Operations/             # Commands a queries
│   │   ├── Homes/
│   │   ├── Rooms/
│   │   └── Devices/
│   ├── Interfaces/             # Repository interfaces
│   └── Enums/                  # Application enums
├── SmartHome.Domain/           # Domain entities
│   ├── Domains/                # Domain models
│   ├── Enums/                  # Domain enums
│   └── Helpers/                # Helper functions
├── SmartHome.Infrastructure/   # Data access layer
│   ├── Repositories/           # Repository implementations
│   ├── Configurations/         # Entity configurations
│   └── Migrations/             # EF Core migrations
└── SmartHome.Test/             # Unit tests
    └── UnitTests/              # Test cases
```

## Logging

Aplikace používá **Serilog** pro structured logging:

- **Information** - Běžné operace
- **Warning** - Upozornění na neobvyklé situace
- **Error** - Chyby v aplikaci

Příklad logu:
```
[14:23:45 INF] [START] Handling CreateHomeCommand
[14:23:46 INF] [END] Handled CreateHomeCommand in 1250ms
```

## Licence

MIT

## Autor

HonzaSo

---

**Poslední aktualizace:** 2026-03-09


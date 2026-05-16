## IoT SmartHome System

The **Internet of Things** (IoT) refers to a network of physical devices embedded with sensors and software to exchange data over the internet. In this project, **SmartDevices** are digital representations of physical hardware.

### Key Features
Rich Domain Modeling: Implements custom logic for real-time energy calculation (Wattage vs. Brightness).

Automated Data Seeding: The database automatically initializes with sample devices (e.g., Kitchen, Bedroom, and Living Room lights) if no data is present.

Responsive Dashboard: A dynamic UI using Angular structural directives (*ngFor) to render real-time device status cards.

### Technical Stack
Backend: .NET 10 / ASP.NET Core API

Frontend: Angular (NgModule Pattern)

Database: SQLite via Entity Framework Core

Language: C# and TypeScript

### Architecture & Device Handling
This application uses a Full-Stack Architecture to manage and display smart device data:

The Virtual Model: The SmartDevice.cs base class defines universal traits shared by all devices, such as unique IDs, online status, and timestamps.

The Control Panel: The Angular client acts as the central hub, fetching data asynchronously via HttpClient.




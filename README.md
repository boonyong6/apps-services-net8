# Apps and Services with .NET 8

# Preface

## What this book covers

### Introduction

- Chapter 1, Introducing Apps and Services with .NET
- **Online-only** section covers:
  - **Benchmark** the performance.
  - Work with types for **reflection** and **attributes**, **expression trees**, and **dynamically generating source code** during compilation process.

### Data (SQL Server-centric)

- Chapter 2, Managing **Relational** Data Using **SQL Server**
  - **SQL Database** is the SQL Server in **Azure**.
  - Learn how to read and write at **low level** using **ADO.NET** for **maximum performance**
  - Then, the ORM named **Dapper** for **ease of development**.
- Chapter 3, Building **Entity Models** for SQL Server Using **EF Core**
  - Using the **higher-level** ORM named **Entity Framework Core (EF Core)**.
- Chapter 4, Managing **NoSQL** Data Using **Azure Cosmos DB**
  - **Online-only** section covers the **graph-based Gremlin API**.

### Libraries (Built-in & third-party)

- Chapter 5, **Multitasking** and **Concurrency**
  - To allow **multiple actions** to occur at the same time by using **threads** and **tasks**.
- Chapter 6, Implementing Popular **Third-Party Libraries**

  - To perform common practical tasks.

  | Library          | Purpose                           |
  | ---------------- | --------------------------------- |
  | Humanizer        | **Formatting** text and numbers.  |
  | ImageSharp       | Manipulating images.              |
  | Serilog          | Logging.                          |
  | AutoMapper       | Mapping objects to other objects. |
  | FluentValidation | Validating data.                  |
  | QuestPDF         | Generating PDFs.                  |

- Chapter 7, Handling **Dates**, **Times**, and **Internationalization**
  - **Noda Time** - Third-party library that supplement the built-in date and time types.

### Services (Backend)

- Chapter 8, Building and Securing Web Services Using **Minimal APIs**
  - The **simplest** way to build web services (**no controller**).
  - Learn how to **improve startup** time and **resources** using native **AOT publish**.
  - Learn how to **protect** and **secure** a **web service**:
    - Rate limiting
    - CORS
    - Authentication and authorization
  - **Test a web service** using the **HTTP editor** in **Visual Studio 2022**, and the **REST Client** extension for **Visual Studio Code**.
  - **Online-only** section covers building services that **quickly expose data models (e.g. EF Core)** using **Open Data Protocol (OData)**.
- Chapter 9, Caching, Queuing, and Resilient Background Services
  - **Adding features to services** that improve **scalability** and **reliability** like **caching** and **queuing**.
  - Learn how to handle **transient problems**.
  - Implement **long-running services** using **background services**.
- Chapter 10, Building **Serverless Nanoservices** Using **Azure Functions**
  - Azure Functions are typically triggered by an activity like:
    - A message sent to a queue.
    - A file uploaded to storage.
    - At a scheduled interval.
- Chapter 11, **Broadcasting** Real-Time Communication Using **SignalR**
  - **Use cases:** notification systems, dashboards (e.g. stock prices)
- Chapter 12, Combining Data Sources Using **GraphQL**
  - Building services that provide a **single endpoint** for **exposing data from multiple sources**.
  - Using the **ChilliCream GraphQL platform** (includes **Hot Chocolate**).
- Chapter 13, Building Efficient Microservices Using **gPRC**
  - Learn about the **`.proto` file format** for defining **service contracts**, and the **Protobuf binary format** for message **serialization**.
  - Learn how to **enable web browsers** to call gRPC services using **gRPC JSON transcoding**.
  - Handling **custom data types** including non-supported types like decimal.
  - Implementing interceptors.
  - Handling faults.

### Apps (Frontend)

- Chapter 14, Building **Web User Interfaces** Using **ASP.NET Core MVC**
  - Razor syntax, tag helpers, and Bootstrap.
- Chapter 15, Building **Web Components** Using **Blazor**
  - Learn how to perform **JavaScript interop** to interact with **browser features** like local storage.
- Chapter 16, Building **Mobile** and **Desktop Apps** Using **.NET MAUI**

### Conclusion

- **Online-only** section introducing the **Survey Project Challenge**.

# 1 Introducing Apps and Services with .NET

- GitHub repo: https://github.com/markjprice/apps-services-net8/
  - Can press the . (dot) key or change `.com` to `.dev` to enter a **live code editor** (based on Visual Studio Code).

## Introducing this books and its contents

### Companion books to continue your learning journey

![1-1-companion-books-for-learning-c#-12-and-net-8](images/1-1-companion-books-for-learning-c.md)

### What you will learn in this book

- Four parts:
  1. Managing **data**
  2. Specialized **libraries** - Can be treated like a **cookbook**, and can read them in any order.
  3. **Service** technologies
  4. **User interface** technologies

### Project naming and port numbering conventions

- Use a **overall name** for the **solution**.
- Use the **type of project** as part of the project name.
- Example naming conventions:

  - Northwind is a fictional company name.

  | Name                              | Type                            | Description                                                                            |
  | --------------------------------- | ------------------------------- | -------------------------------------------------------------------------------------- |
  | `Northwind.Common`                | Class library                   | For **common types**, used across multiple projects.                                   |
  | `Northwind.Common.EntityModels`   | Class library                   | For common **EF Core entity models**.                                                  |
  | `Northwind.Common.DataContext`    | Class library                   | For the EF Core database context **with dependencies on specific database providers**. |
  | `Northwind.Mvc`                   | ASP.NET Core MVC                | For a complex **website** that can be more **easily unit tested**.                     |
  | `Northwind.WebApi.Service`        | ASP.NET Core                    | For an **HTTP API service**. A good choice for **integrating with websites**.          |
  | `Northwind.WebApi.Client.Console` | Console app                     | A client to a web service.                                                             |
  | `Northwind.gRPC.Service`          | ASP.NET Core                    | For a gRPC service.                                                                    |
  | `Northwind.gRPC.Client.Mvc`       | ASP.NET Core MVC                | A client to a gRPC service.                                                            |
  | `Northwind.BlazorWasm.Client`     | ASP.NET Core Blazor WebAssembly | Client-side Blazor project.                                                            |
  | `Northwind.BlazorWasm.Server`     | ASP.NET Core Blazor WebAssembly | Server-side Blazor project.                                                            |
  | `Northwind.BlazorWasm.Shared`     | Class library                   | Used by client- and server-side Blazor projects.                                       |

### Treating warnings as errors

- Compiler **warnings** are **potential problems**.
- **Ignoring warning** encourages **poor** development **practices**.
- Can configure a **project setting** to be forced to fix warnings.

  ```csharp
  // .csproj
  <Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
      <OutputType>Exe</OutputType>
      <TargetFramework>net8.0</TargetFramework>
      <ImplicitUsings>enable</ImplicitUsings>
      <Nullable>enable</Nullable>
      <TreatWarningsAsErrors>true</TreatWarningsAsErrors>  // <--
    </PropertyGroup>
  // ...
  ```

- **Good practice:** **Always** treat warnings as errors (**except for gRPC projects** until Google updates their code generation tools).
- Learn more at https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings#warningsaserrors-and-warningsnotaserrors

## App and service technologies

- Microsoft calls platforms for building:
  - Applications - App models
  - Services - Workloads

### Building websites and apps using ASP.NET Core

- Technologies for building websites:

  | Technology               | Description                                                                         |
  | ------------------------ | ----------------------------------------------------------------------------------- |
  | ASP.NET Core Razor Pages | For **simple** websites.                                                            |
  | ASP.NET Core MVC         | Popular for developing **complex** websites.                                        |
  | Razor class libraries    | **Package** reusable functionality (e.g. UI components) for ASP.NET Core projects.  |
  | Blazor                   | To build UI components (WebAssembly) using C# and .NET instead of using JavaScript. |

- Note: Blazor can also be used to create **hybrid mobile and desktop apps** when combined with **.NET MAUI**.

### Building web and other services

- Services are sometimes described based on their complexity:

  | Type         | Description                                                                                                                             |
  | ------------ | --------------------------------------------------------------------------------------------------------------------------------------- |
  | Service      | - All functionality in one monolithic service.                                                                                          |
  | Microservice | - Multiple services that each focus on smaller set of functionalities, and each should own its own data.                                |
  | Nanoservice  | - Function as a service.<br />- **Often inactive** until called upon to reduce resources and costs.<br />- Aka **serverless** services. |

### Windows Communication Foundation (WCF)

- **Abstracts the business logic from the communication technology infrastructure** so that you could switch to an alternative or even have multiple mechanisms to communicate with the service.
- Uses XML configuration to declaratively define endpoints.
- Community-owned OSS project - **CoreWCF**
  - It can never be a full port since parts of WCF are Windows-specific.
- Technologies like WCF allow for building of **distributed applications**.
  - Alternative RPC technology - **gRPC**

### Common service principles

- To make method calls **chunky** instead of chatty (bundle all the data needed for an operation in a single call).
- The **overhead of a remote call** is one of the biggest **negative effects** of services.
- Having **smaller services** can **hugely negatively impact** a solution architecture.

### Summary of choices for services

- Each service technology has its **pros** and **cons** based on its **feature support**:

  | Feature                                         | Web API     | OData       | GraphQL               | gRPC    | SignalR   |
  | ----------------------------------------------- | ----------- | ----------- | --------------------- | ------- | --------- |
  | Clients can request **just the data they need** | No          | Yes         | Yes                   | No      | No        |
  | Minimum HTTP version                            | 1.1         | 1.1         | 1.1                   | **2.0** | 1.1       |
  | Browser support                                 | Yes         | Yes         | Yes                   | No      | Yes       |
  | **Data** format                                 | XML, JSON   | XML, JSON   | GraphQL (**JSONish**) | Binary  | Varies    |
  | Service **documentation**                       | Swagger     | Swagger     | No                    | No      | No        |
  | Code generation                                 | Third-party | Third-party | Third-party           | Google  | Microsoft |
  | Caching                                         | Easy        | Easy        | Hard                  | Hard    | Hard      |

- Recommendations for various scenarios:

  | Scenario                                      | Recommendation                                                                                                                                                                                                     |
  | --------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
  | **Public** service                            | - HTTP/**1.1**-based services.<br />- Clients: browser, mobile                                                                                                                                                     |
  | **Public data** service                       | - **OData**, **GraphQL** - Good choices for exposing **complex datasets** (e.g. come from different data stores).                                                                                                  |
  | Service-to-service                            | - **gRPC** - Low-latency, high-throughput.<br />- Great for lightweight internal **microservices**.                                                                                                                |
  | **Point-to-point** real-time communication    | - **gRPC** - Support for **bidirectional streaming**. Can **push** messages in real time **without polling**.<br />- **SignalR** - General real-time solution. **Easier** to implement than gRPC (less efficient). |
  | **Broadcast** real-time communication         | - **SignalR** - Great support for broadcasting.                                                                                                                                                                    |
  | **Polyglot** environment                      | - **gRPC** - Tooling supports all popular languages.                                                                                                                                                               |
  | **Network-bandwidth-constrained** environment | - **gRPC** - Lightweight message format (Protobuf).                                                                                                                                                                |
  | Serverless nanoservice                        | - **Azure Functions** - Good choice for services that **don't need to be running constantly**.                                                                                                                     |

## Setting up your development environment

### Keyboard shortcuts

- [Visual Studio](appendices/visual-studio-keyboard-shortcuts.pdf) (Can be configured to use vscode keyboard mapping scheme)
- [vscode](appendices/vscode-keyboard-shortcuts.pdf)
- [SSMS](appendices/ssms-keyboard-shortcuts.pdf)

### Consuming Azure resources

- Azure resources to use and their local development alternative:

  | Azure resource        | Local development alternative                                                                      |
  | --------------------- | -------------------------------------------------------------------------------------------------- |
  | SQL Database          | - SQL Server Developer Edition on Windows.<br />- SQL Edge in a Docker container (cross-platform). |
  | Cosmos DB database    | Azure Cosmos DB emulator.                                                                          |
  | Azure functions       | Azurite open-source emulator.                                                                      |
  | Azure SignalR Service | Add SignalR to any ASP.NET Core project.                                                           |

## Benchmarking Performance and Testing

### Monitoring performance and memory resource usage using built-in types

- Before improving the performance of any code, we should monitor its speed to record a **baseline**.

#### Evaluating the efficiency of types

- Factors to determine the best types to use:
  - Functionality
  - Memory size
  - Performance
  - Future needs
- `sizeof()` shows the number of bytes that a single instance (primitive types) uses.

#### Monitor performance and memory using diagnostics

- `System.Diagnostics` namespace

#### Useful members of the `Stopwatch` and `Process` types

- `Stopwatch` type:

  | Member                         | Description                                                  |
  | ------------------------------ | ------------------------------------------------------------ |
  | `Restart` method               | Resets the elapsed time and starts the timer.                |
  | `Stop` method                  | Stops the timer.                                             |
  | `Elapsed` property             | Elapsed time stored as a `TimeSpan`.                         |
  | `ElapsedMilliseconds` property | Elapsed time **in milliseconds** stored as an `Int64` value. |

- `Process` type:

  | Member                | Description                                                      |
  | --------------------- | ---------------------------------------------------------------- |
  | `VirtualMemorySize64` | Displays the amount of **virtual memory**, in bytes, allocated.  |
  | `WorkingSet64`        | Displays the amount of **physical memory**, in bytes, allocated. |

#### Implementing a `Recorder` class (Utils)

- To monitor time and memory resource usage.

#### Measuring the efficiency of processing strings

- To evaluate the best way to process `string` variables.
- **Good Practice:** Avoid using the `String.Concat` method or the `+` operator inside loops. Use `StringBuilder` instead.

### Monitoring performance and memory using `Benchmark.NET`

#### Building a console app with `Benchmark.NET`

- Define a class with **methods** for each benchmark.
- `Important:` Must build in a **Release** build for performance testing, as most optimizations are disabled in **Debug** builds.

## Observing and Modifying Code Execution Dynamically

- About **types** for:
  - Performing **code reflection** and **applying and reading attributes**
  - Working with **expression trees**
  - Creating **source generators**

### Using an analyzer to write better code - StyleCop

- Steps to setup StyleCop:

  1. Add the `StyleCop.Analyzers` package reference.
  2. Add a `stylecop.json` for configuring StyleCop settings.

     ```json
     {
       "$schema": "https://raw.githubusercontent.com/DotNetAnalyzers/StyleCopAnalyzers/master/StyleCop.Analyzers/StyleCop.Analyzers/Settings/stylecop.schema.json",
       "settings": {}
     }
     ```

  3. In the `.csproj`, configure the `stylecop.json`:
     - To **not be included** in published **deployments**.
     - To **enable it** as an additional file **for processing during development**.

#### Suppressing warnings

- Three ways:

  1. Setting an **assembly-level attribute**

     ```cs
     // GlobalSuppressions.cs

     [assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1200:UsingDirectivesMustBePlacedWithinNamespace", Justification = "Reviewed.")]
     ```

  2. Using `#pragma` statements

     ```cs
     #pragma warning disable SA1200
     using System.Diagnostics;
     #pragma warning restore SA1200
     ```

  3. Add a configuration option in `stylecop.json`

     ```json
     {
       "$schema": "https://raw.githubusercontent.com/DotNetAnalyzers/StyleCopAnalyzers/master/StyleCop.Analyzers/StyleCop.Analyzers/Settings/stylecop.schema.json",
       "settings": {
         "orderingRules": {
           "usingDirectivesPlacement": "outsideNamespace"
         }
       }
     }
     ```

#### Fixing the code

- **Project property** for generating an **XML file for documentation**.

  - The XML file can then be processed by a tool like **DocFX** to convert it into documentation files.
  - Reference: [DocFX guide](https://www.jamescroft.co.uk/building-net-project-docs-with-docfx-on-github-pages/)

  ```xml
  <!-- .csproj -->
  <PropertyGroup>
    ...
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>
  ```

#### Understanding common StyleCop recommendations

- Order inside a code file:

  1. External alias directives
  2. Using directives
  3. Namespaces
  4. Delegates
  5. Enums
  6. Interfaces
  7. Structs
  8. Classes

- Order within a class:

  1. Fields
  2. Constructors
  3. Destructors (finalizers)
  4. Delegates
  5. Events
  6. Enums
  7. Interfaces
  8. Properties
  9. Indexers
  10. Methods
  11. Structs
  12. Nested classes and records

- Reference: [StyleCop rules](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/DOCUMENTATION.md)

### Working with reflection and attributes

- **Reflection** is a feature that allows code to **manipulate itself**.
- **Assembly** is made up of four parts:
  1. Assembly metadata - Name, file version, referenced assemblies
  2. Type metadata
  3. IL code
  4. Embedded resources (optional) - Images, Javascript
- **Attributes** can be applied at multiple levels:

  ```cs
  // an assembly-level attribute
  [assembly: AssemblyTitle("Working with reflection and attributes")]

  // a type-level attribute
  [Serializable]
  public class Person
  {
    // a member-level attribute
    [Obsolete("Deprecated: use Run instead.")]
    public void Walk()
    {
  ...
  ```

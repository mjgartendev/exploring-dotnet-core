# Creating a Project using dotnet CLI Tools

## Prerequisites

Install the following:

- [.NET Core 2.1 SDK or later](https://www.microsoft.com/net/download/all)
- [Visual Studio Code](https://code.visualstudio.com/download)
- [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)

## Create the project

From a console, run the following commands:

```bash
dotnet new webapi -o TodoApi
cd TodoApi
```

> TIP: run `dotnet new` to see a list of available templates

## Run the project

Project should restore dependencies automatically, but if not you will need to restore them manually.

```bash
# if packages weren't automatically restored
dotnet restore

# launch app - will call the web host builder with current env startup config
dotnet run
```

## Add DB Support (EF Core)

## Apply Migrations (EF Core)

## Build

## Publish

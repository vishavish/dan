# dan

**dan** is a command line utility that analyzes a directory, display file types and their disk usage written in C#.

## Usage

```
USAGE: dan <directory> [OPTION]
OPTIONS
    -h Show this help message
    -r Search recursively
```

## Compiling from source

**NOTE**: This project requires the [.NET SDK](https://dotnet.microsoft.com/en-us/download).

1. Clone the repository:
```
git clone https://github.com/vishavish/dan.git
```

2.
> To run:
```
cd src
dotnet run
```

> To build:

On Windows:
```
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true --output build/windows
```
On Linux:
```
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true --output build/linux
```

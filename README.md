# Rapid Pay API Service

This is a service that allows you to make payments using the Rapid Pay API.

## Requirements

* Docker
* EF
* .Net 8.0

## Installation

1. Clone the repository
2. Run the shell script SqlDocker.sh to start the SQL Server container
3. Install EF Migrations (dotnet tool install --global dotnet-ef)
4. Apply Migrations 
   * dotnet ef database update
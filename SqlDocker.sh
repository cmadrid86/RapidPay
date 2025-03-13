#!/bin/bash

docker pull mcr.microsoft.com/azure-sql-edge
docker container ls
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=l0calpass." -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sql mcr.microsoft.com/azure-sql-edge
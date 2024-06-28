#!/bin/bash

# Run DbMigrator
cd /app/dbmigrator
dotnet Oakell.DbMigrator.dll

# Run HttpApi.Host
cd /app
dotnet Oakell.HttpApi.Host.dll
#!/bin/bash

# Run DbMigrator
cd /app/Oakell.DbMigrator
dotnet Oakell.DbMigrator.dll

# Run HttpApi.Host
cd /app/Oakell.HttpApi.Host
dotnet Oakell.HttpApi.Host.dll

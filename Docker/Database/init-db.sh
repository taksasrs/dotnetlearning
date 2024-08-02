#!/bin/bash

# Wait for the SQL Server to start up
sleep 50s

# Run the initialization SQL script
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P srs@2024 -d master -i /usr/src/app/init-db.sql

# Use the official Microsoft SQL Server 2019 image from the Docker Hub
FROM mcr.microsoft.com/mssql/server:2022-CU13-ubuntu-22.04

# Set environment variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=srs@2024

# Switch to root user to perform file operations
USER root

# # Copy initialization script to the container
COPY ./Database/init-db.sql /usr/src/app/init-db.sql

# Copy initialization script to the container
COPY ./Database/init-db.sh /usr/src/app/init-db.sh

# Grant permissions for the init script to be executable
RUN chmod +x /usr/src/app/init-db.sh

# Switch back to mssql user
USER mssql

# Run the SQL Server process
CMD /bin/bash /usr/src/app/init-db.sh & /opt/mssql/bin/sqlservr

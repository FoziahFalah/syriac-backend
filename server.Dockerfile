# Use Bitnami's dotnet-sdk image as the base image
FROM bitnami/dotnet-sdk

# Set the working directory
WORKDIR /app

# Create a directory for the server
RUN mkdir server

# Copy the server application files
COPY ./syriacsources-backend/ /app/syriacsources-backend

# Set the working directory to the server directory
WORKDIR /app/syriacsources-backend/src/Web

# Restore dependencies
RUN dotnet restore

# Expose the port if needed
EXPOSE 5106

# Start the application using CMD
CMD ["dotnet", "run", "--urls", "http://0.0.0.0:5106"]
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY GroceryManagement.API/*.csproj ./GroceryManagement.API/
RUN dotnet restore GroceryManagement.API/GroceryManagement.API.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/GroceryManagement.API
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Set environment variable to ensure app listens on port 8087
ENV ASPNETCORE_URLS=http://+:8087

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose port 8087
EXPOSE 8087

# Start the application
ENTRYPOINT ["dotnet", "GroceryManagement.API.dll"]

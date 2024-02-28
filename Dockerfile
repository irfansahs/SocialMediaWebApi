# Set the base image to use for this container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory in the container
WORKDIR /app

# Copy and restore domain layer
COPY Media.Domain/Media.Domain.csproj ./Media.Domain/
RUN dotnet restore ./Media.Domain/Media.Domain.csproj

# Copy and restore application layer
COPY Media.Application/Media.Application.csproj ./Media.Application/
RUN dotnet restore ./Media.Application/Media.Application.csproj

# Copy and restore infrastructure layer
COPY Media.Infrastructure/Media.Infrastructure.csproj ./Media.Infrastructure/
RUN dotnet restore ./Media.Infrastructure/Media.Infrastructure.csproj

# Copy and restore persistence layer
COPY Media.Persistence/Media.Persistence.csproj ./Media.Persistence/
RUN dotnet restore ./Media.Persistence/Media.Persistence.csproj

# Copy and restore web api layer
COPY Media.WebApi/Media.WebApi.csproj ./Media.WebApi/
RUN dotnet restore ./Media.WebApi/Media.WebApi.csproj

# Copy the remaining files and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Define the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Start the application
ENTRYPOINT ["dotnet", "Media.WebApi.dll"]

# Stage 1: Build the application
# Use the official .NET 8 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies first.
# This leverages Docker's layer caching to speed up future builds.
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application source code
COPY . .

# Build and publish the application for release
RUN dotnet publish -c Release -o /app/publish --no-restore

# Stage 2: Create the final, smaller runtime image
# Use the official ASP.NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose the port the application will listen on.
# This should match the ASPNETCORE_URLS port inside the container.
EXPOSE 8080

# Configure the container to run as a non-root user for better security
USER app

# Set the entrypoint for the container.
# IMPORTANT: Replace 'YourProject.Api.dll' with the actual name of your project's DLL file.
ENTRYPOINT ["dotnet", "simple-tools-api.dll"]
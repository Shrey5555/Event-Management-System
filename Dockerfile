# Use official .NET runtime for running the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use .NET SDK for building the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EventManagement/EventManagement.csproj", "EventManagement/"]
RUN dotnet restore "EventManagement/EventManagement.csproj"
COPY . .
WORKDIR "/src/EventManagement"
RUN dotnet publish -c Release -o /app/publish

# Final stage: Copy built files and set entry point
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EventManagement.dll"]

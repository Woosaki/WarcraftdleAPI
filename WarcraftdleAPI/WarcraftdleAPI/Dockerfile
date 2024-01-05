#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WarcraftdleAPI/WarcraftdleAPI.csproj", "WarcraftdleAPI/"]
COPY ["WarcraftdleAPI.Application/WarcraftdleAPI.Application.csproj", "WarcraftdleAPI.Application/"]
COPY ["WarcraftdleAPI.Domain/WarcraftdleAPI.Domain.csproj", "WarcraftdleAPI.Domain/"]
COPY ["WarcraftdleAPI.Infrastructure/WarcraftdleAPI.Infrastructure.csproj", "WarcraftdleAPI.Infrastructure/"]
RUN dotnet restore "./WarcraftdleAPI/./WarcraftdleAPI.csproj"
COPY . .
WORKDIR "/src/WarcraftdleAPI"
RUN dotnet build "./WarcraftdleAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WarcraftdleAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarcraftdleAPI.dll"]
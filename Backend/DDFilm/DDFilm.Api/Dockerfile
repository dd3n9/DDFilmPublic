#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DDFilm.Api/DDFilm.Api.csproj", "DDFilm.Api/"]
COPY ["DDFilm.Application/DDFilm.Application.csproj", "DDFilm.Application/"]
COPY ["DDFilm.Contracts/DDFilm.Contracts.csproj", "DDFilm.Contracts/"]
COPY ["DDFilm.Domain/DDFilm.Domain.csproj", "DDFilm.Domain/"]
COPY ["DDFilm.Infrastructure/DDFilm.Infrastructure.csproj", "DDFilm.Infrastructure/"]
RUN dotnet restore "./DDFilm.Api/./DDFilm.Api.csproj"
COPY . .
WORKDIR "/src/DDFilm.Api"
RUN dotnet build "./DDFilm.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DDFilm.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDFilm.Api.dll"]
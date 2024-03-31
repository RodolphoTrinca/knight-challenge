#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Knight.API/Knight.API.csproj", "Knight.API/"]
COPY ["Knight.Infra/Knight.Infra.csproj", "Knight.Infra/"]
COPY ["Knight.Application/Knight.Application.csproj", "Knight.Application/"]
RUN dotnet restore "./Knight.API/Knight.API.csproj"
COPY . .
WORKDIR "/src/Knight.API"
RUN dotnet build "./Knight.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Knight.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Knight.API.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ThundersTasks.API/ThundersTasks.API.csproj", "ThundersTasks.API/"]
RUN dotnet restore "./ThundersTasks.API/ThundersTasks.API.csproj"
COPY . .
WORKDIR "/src/ThundersTasks.API"
RUN dotnet build "./ThundersTasks.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ThundersTasks.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThundersTasks.API.dll"]
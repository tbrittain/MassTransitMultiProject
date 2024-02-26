FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MassTransitMultiProject.WebApi/MassTransitMultiProject.WebApi.csproj", "MassTransitMultiProject.WebApi/"]
RUN dotnet restore "MassTransitMultiProject.WebApi/MassTransitMultiProject.WebApi.csproj"
COPY . .
WORKDIR "/src/MassTransitMultiProject.WebApi"
RUN dotnet build "MassTransitMultiProject.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MassTransitMultiProject.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransitMultiProject.WebApi.dll"]

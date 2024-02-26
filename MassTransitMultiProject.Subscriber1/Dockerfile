FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MassTransitMultiProject.Subscriber1/MassTransitMultiProject.Subscriber1.csproj", "MassTransitMultiProject.Subscriber1/"]
RUN dotnet restore "MassTransitMultiProject.Subscriber1/MassTransitMultiProject.Subscriber1.csproj"
COPY . .
WORKDIR "/src/MassTransitMultiProject.Subscriber1"
RUN dotnet build "MassTransitMultiProject.Subscriber1.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MassTransitMultiProject.Subscriber1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransitMultiProject.Subscriber1.dll"]

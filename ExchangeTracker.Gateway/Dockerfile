FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExchangeTracker.Gateway/ExchangeTracker.Gateway.csproj", "ExchangeTracker.Gateway/"]
COPY ["Implementations/Exchanges/BinanceExchangeClient/BinanceExchangeClient.csproj", "Implementations/Exchanges/BinanceExchangeClient/"]
COPY ["Abstractions/ExchangeClients.Abstractions/ExchangeClients.Abstractions.csproj", "Abstractions/ExchangeClients.Abstractions/"]
COPY ["Abstractions/Core.Abstractions/Core.Abstractions.csproj", "Abstractions/Core.Abstractions/"]
COPY ["Implementations/Exchanges/BybitExchangeClient/BybitExchangeClient.csproj", "Implementations/Exchanges/BybitExchangeClient/"]
COPY ["Implementations/ExchangeTracker.Core/ExchangeTracker.Core.csproj", "Implementations/ExchangeTracker.Core/"]
COPY ["Abstractions/DAL.Abstractions/DAL.Abstractions.csproj", "Abstractions/DAL.Abstractions/"]
COPY ["Implementations/ExchangeTracker.DAL/ExchangeTracker.DAL.csproj", "Implementations/ExchangeTracker.DAL/"]
RUN dotnet restore "ExchangeTracker.Gateway/ExchangeTracker.Gateway.csproj"
COPY . .
WORKDIR "/src/ExchangeTracker.Gateway"
RUN dotnet build "./ExchangeTracker.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExchangeTracker.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangeTracker.Gateway.dll"]

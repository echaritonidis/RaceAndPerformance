FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build

RUN dotnet dev-certs https

WORKDIR /src
COPY ["RaceAndPerformance/Api/RaceAndPerformance.Api.csproj", "RaceAndPerformance/Api/"]
COPY ["RaceAndPerformance/Core/RaceAndPerformance.Core.csproj", "RaceAndPerformance/Core/"]
COPY ["RaceAndPerformance/Dal/RaceAndPerformance.Dal.csproj", "RaceAndPerformance/Dal/"]
COPY ["RaceAndPerformance/Application/RaceAndPerformance.Application.csproj", "RaceAndPerformance/Application/"]
RUN dotnet restore "RaceAndPerformance/Api/RaceAndPerformance.Api.csproj"
COPY . .

WORKDIR "/src/RaceAndPerformance/Api"
RUN dotnet build "RaceAndPerformance.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RaceAndPerformance.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "RaceAndPerformance.Api.dll"]

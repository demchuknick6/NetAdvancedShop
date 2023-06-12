#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Catalog/Catalog.WebUI/Catalog.WebUI.csproj", "src/Catalog/Catalog.WebUI/"]
COPY ["src/Common/Common.Infrastructure.EventBus.RabbitMQ/Common.Infrastructure.EventBus.RabbitMQ.csproj", "src/Common/Common.Infrastructure.EventBus.RabbitMQ/"]
COPY ["src/Common/Common.Infrastructure.EventBus/Common.Infrastructure.EventBus.csproj", "src/Common/Common.Infrastructure.EventBus/"]
COPY ["src/Common/Common.Infrastructure.EventBus.Events/Common.Infrastructure.EventBus.Events.csproj", "src/Common/Common.Infrastructure.EventBus.Events/"]
COPY ["src/Common/Common.Rbac/Common.Rbac.csproj", "src/Common/Common.Rbac/"]
COPY ["src/Common/Common.WebUI/Common.WebUI.csproj", "src/Common/Common.WebUI/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Catalog/Catalog.Application.ApplicationEvents/Catalog.Application.ApplicationEvents.csproj", "src/Catalog/Catalog.Application.ApplicationEvents/"]
COPY ["src/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "src/Catalog/Catalog.Infrastructure/"]
COPY ["src/Catalog/Catalog.Application/Catalog.Application.csproj", "src/Catalog/Catalog.Application/"]
COPY ["src/Catalog/Catalog.Domain/Catalog.Domain.csproj", "src/Catalog/Catalog.Domain/"]

RUN dotnet restore "src/Catalog/Catalog.WebUI/Catalog.WebUI.csproj"

COPY . .
WORKDIR "/src/src/Catalog/Catalog.WebUI"
RUN dotnet build "Catalog.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.WebUI.csproj" --no-restore -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetAdvancedShop.Catalog.WebUI.dll"]

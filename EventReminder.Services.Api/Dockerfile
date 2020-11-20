FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY ["EventReminder.Domain/EventReminder.Domain.csproj", "EventReminder.Domain/"]
COPY ["EventReminder.Contracts/EventReminder.Contracts.csproj", "EventReminder.Contracts/"]
COPY ["EventReminder.Application/EventReminder.Application.csproj", "EventReminder.Application/"]
COPY ["EventReminder.Infrastructure/EventReminder.Infrastructure.csproj", "EventReminder.Infrastructure/"]
COPY ["EventReminder.Persistence/EventReminder.Persistence.csproj", "EventReminder.Persistence/"]
COPY ["EventReminder.Services.Api/EventReminder.Services.Api.csproj", "EventReminder.Services.Api/"]
RUN dotnet restore "EventReminder.Services.Api/EventReminder.Services.Api.csproj"

COPY . .
WORKDIR "/EventReminder.Services.Api"
FROM build AS publish
RUN dotnet publish "EventReminder.Services.Api.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventReminder.Services.Api.dll"]
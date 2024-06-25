FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventReminder.Services.Notifications/EventReminder.Services.Notifications.csproj", "EventReminder.Services.Notifications/"]
COPY ["EventReminder.BackgroundTasks/EventReminder.BackgroundTasks.csproj", "EventReminder.BackgroundTasks/"]
COPY ["EventReminder.Infrastructure/EventReminder.Infrastructure.csproj", "EventReminder.Infrastructure/"]
COPY ["EventReminder.Application/EventReminder.Application.csproj", "EventReminder.Application/"]
COPY ["EventReminder.Contracts/EventReminder.Contracts.csproj", "EventReminder.Contracts/"]
COPY ["EventReminder.Domain/EventReminder.Domain.csproj", "EventReminder.Domain/"]
COPY ["EventReminder.Persistence/EventReminder.Persistence.csproj", "EventReminder.Persistence/"]
RUN dotnet restore "./EventReminder.Services.Notifications/EventReminder.Services.Notifications.csproj"
COPY . .
WORKDIR "/src/EventReminder.Services.Notifications"
RUN dotnet build "./EventReminder.Services.Notifications.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventReminder.Services.Notifications.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventReminder.Services.Notifications.dll"]
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-notif
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/core/aspnet

WORKDIR /app
COPY --from=build-notif /app/out .
ENTRYPOINT ["dotnet", "notification_handler.dll"]

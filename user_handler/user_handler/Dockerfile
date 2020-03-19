FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-user
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/core/aspnet

WORKDIR /app
COPY --from=build-user /app/out .
ENTRYPOINT ["dotnet", "user_handler.dll"]

#parent image, buat template, dipake buat restore Nama build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#template dibuat di
WORKDIR /source

#diisi ini
COPY *.sln .
#dicopy ka keataas
COPY user_handler/*.csproj ./user_handler/
#ngejalanin dotnet
RUN dotnet restore

#
COPY . .
WORKDIR "/source/user_handler"
RUN dotnet publish "user_handler.csproj" -c Release -o /app

#
FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app

COPY --from=publish /app ./
ENTRYPOINT ["dotnet", "user_handler.dll"]

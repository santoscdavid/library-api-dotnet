#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["LivrariaAPI/LivrariaAPI.csproj", "LivrariaAPI/"]
RUN dotnet restore "LivrariaAPI/LivrariaAPI.csproj"
COPY . .
WORKDIR "/src/LivrariaAPI"
RUN dotnet build "LivrariaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LivrariaAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LivrariaAPI.dll"]
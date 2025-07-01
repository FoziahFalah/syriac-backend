#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
FROM mcr.microsoft.com/dotnet/sdk:8.0.204 AS build
WORKDIR /src
COPY Directory.Packages.props ./
COPY Directory.Build.props ./
COPY Web/Web.csproj Web/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
WORKDIR /src/Web
RUN dotnet restore Web - Copy.csproj
COPY . .
RUN dotnet build Web.csproj -c Release -o /app/build
FROM build AS publish
RUN dotnet publish Web.csproj -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]


















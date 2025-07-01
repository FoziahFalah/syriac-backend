#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# ===== ASP.NET Core 8.0 =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
# ===== SDK Build Stage =====
FROM mcr.microsoft.com/dotnet/sdk:8.0.204 AS build
WORKDIR /src
# ===== Copy project files =====
COPY Directory.Packages.props ./
COPY Directory.Build.props ./
COPY SyriacSources.Backend.Web/Web.csproj SyriacSources.Backend.Web/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
# ===== Restore =====
RUN dotnet restore ./SyriacSources.Backend.Web/Web.csproj
# ===== Copy full source code =====
COPY . .
# ===== Build =====
WORKDIR /src/SyriacSources.Backend.Web
RUN dotnet build Web.csproj -c Release -o /app/build
# ===== Publish =====
FROM build AS publish
RUN dotnet publish Web.csproj -c Release -o /app/publish /p:UseAppHost=false
# ===== Final Image =====
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]













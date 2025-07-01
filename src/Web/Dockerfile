#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# ?????? ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
# ?????? SDK ??????
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# ???? ????? ???????
COPY Directory.Packages.props ./
COPY Directory.Build.props ./
COPY SyriacSources.Backend.Web/SyriacSources.Backend.Web.csproj SyriacSources.Backend.Web/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
# ??????? ?????
RUN dotnet restore SyriacSources.Backend.Web/SyriacSources.Backend.Web.csproj
# ???? ????? ???
COPY . .
# ???? ???????
WORKDIR /src/SyriacSources.Backend.Web
RUN dotnet build SyriacSources.Backend.Web.csproj -c Release -o /app/build
# ??? ???????
FROM build AS publish
RUN dotnet publish SyriacSources.Backend.Web.csproj -c Release -o /app/publish /p:UseAppHost=false
# ?????? ????????
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SyriacSources.Backend.Web.dll"]







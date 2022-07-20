#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Bebrand.Services.Api/Bebrand.Services.Api.csproj", "Bebrand.Services.Api/"]
COPY ["Bebrand.Infra.CrossCutting.IoC/Bebrand.Infra.CrossCutting.IoC.csproj", "Bebrand.Infra.CrossCutting.IoC/"]
COPY ["Bebrand.Application/Bebrand.Application.csproj", "Bebrand.Application/"]
COPY ["Bebrand.Infra.Data/Bebrand.Infra.Data.csproj", "Bebrand.Infra.Data/"]
COPY ["Bebrand.Domain.Core/Bebrand.Domain.Core.csproj", "Bebrand.Domain.Core/"]
COPY ["Bebrand.Domain/Bebrand.Domain.csproj", "Bebrand.Domain/"]
COPY ["Bebrand.Infra.CrossCutting.Bus/Bebrand.Infra.CrossCutting.Bus.csproj", "Bebrand.Infra.CrossCutting.Bus/"]
COPY ["Bebrand.Infra.CrossCutting.Identity/Bebrand.Infra.CrossCutting.Identity.csproj", "Bebrand.Infra.CrossCutting.Identity/"]
RUN dotnet restore "Bebrand.Services.Api/Bebrand.Services.Api.csproj"
COPY . .
WORKDIR "/src/Bebrand.Services.Api"
RUN dotnet build "Bebrand.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bebrand.Services.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bebrand.Services.Api.dll"]

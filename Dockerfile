#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Bebrand.Services.Api/Bebrand.Services.Api.csproj", "Bebrand.Services.Api/"]
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
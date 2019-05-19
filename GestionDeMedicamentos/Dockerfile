FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["GestionDeMedicamentos/GestionDeMedicamentos.csproj", "GestionDeMedicamentos/"]
RUN dotnet restore "GestionDeMedicamentos/GestionDeMedicamentos.csproj"
COPY . .
WORKDIR "/src/GestionDeMedicamentos"
RUN dotnet build "GestionDeMedicamentos.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GestionDeMedicamentos.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GestionDeMedicamentos.dll"]
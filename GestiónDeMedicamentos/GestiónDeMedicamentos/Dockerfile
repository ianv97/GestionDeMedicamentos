FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["GestiónDeMedicamentos/GestiónDeMedicamentos.csproj", "GestiónDeMedicamentos/"]
RUN dotnet restore "GestiónDeMedicamentos/GestiónDeMedicamentos.csproj"
COPY . .
WORKDIR "/src/GestiónDeMedicamentos"
RUN dotnet build "GestiónDeMedicamentos.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GestiónDeMedicamentos.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GestiónDeMedicamentos.dll"]
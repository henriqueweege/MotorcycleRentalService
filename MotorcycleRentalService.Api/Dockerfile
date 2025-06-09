FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MotorcycleRentalService.Api/MotorcycleRentalService.Api.csproj", "MotorcycleRentalService.Api/"]
COPY ["MotorcycleRentalService.Application/MotorcycleRentalService.Application.csproj", "MotorcycleRentalService.Application/"]
COPY ["MotorcycleRentalService.Domain/MotorcycleRentalService.Domain.csproj", "MotorcycleRentalService.Domain/"]
COPY ["MotorcycleRentalService.Infrastructure/MotorcycleRentalService.Infrastructure.csproj", "MotorcycleRentalService.Infrastructure/"]
RUN dotnet restore "./MotorcycleRentalService.Api/MotorcycleRentalService.Api.csproj"
COPY . .
WORKDIR "/src/MotorcycleRentalService.Api"
RUN dotnet build "./MotorcycleRentalService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MotorcycleRentalService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotorcycleRentalService.Api.dll"]
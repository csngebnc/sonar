#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["services/Shipping/Shipping.Api/Shipping.Api.csproj", "services/Shipping/Shipping.Api/"]
RUN dotnet restore "services/Shipping/Shipping.Api/Shipping.Api.csproj"
COPY . .
WORKDIR "/src/services/Shipping/Shipping.Api"
RUN dotnet build "Shipping.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shipping.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shipping.Api.dll"]
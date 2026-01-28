FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MovieSearch.API.csproj", "./"]
RUN dotnet restore "MovieSearch.API.csproj"
COPY . .
RUN dotnet build "MovieSearch.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieSearch.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "MovieSearch.API.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:9.0-nanoserver-ltsc2022 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0-nanoserver-ltsc2022 AS build
ARG configuration=Release
WORKDIR /src
COPY ["MyStandaloneApp/MyStandaloneApp.csproj", "MyStandaloneApp/"]
RUN dotnet restore "MyStandaloneApp\MyStandaloneApp.csproj"
COPY . .
WORKDIR "/src/MyStandaloneApp"
RUN dotnet build "MyStandaloneApp.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "MyStandaloneApp.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyStandaloneApp.dll"]

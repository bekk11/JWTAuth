FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JWTAuthentication.WebAPI/JWTAuthentication.WebAPI.csproj", "JWTAuthentication.WebAPI/"]
RUN dotnet restore "JWTAuthentication.WebAPI/JWTAuthentication.WebAPI.csproj"
COPY . .
WORKDIR "/src/JWTAuthentication.WebAPI"
RUN dotnet build "JWTAuthentication.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "JWTAuthentication.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTAuthentication.WebAPI.dll"]

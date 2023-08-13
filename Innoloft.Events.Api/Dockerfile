FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Innoloft.Events.Api/Innoloft.Events.Api.csproj", "Innoloft.Events.Api/"]
RUN dotnet restore "Innoloft.Events.Api/Innoloft.Events.Api.csproj"
COPY . .
WORKDIR "/src/Innoloft.Events.Api"
RUN dotnet build "Innoloft.Events.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Innoloft.Events.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Innoloft.Events.Api.dll"]

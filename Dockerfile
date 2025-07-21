#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.16-amd64 AS base
WORKDIR /app
RUN apk update
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk update && apk upgrade && apk add \
    libcap \
    && setcap 'cap_net_bind_service=+ep' /usr/share/dotnet/dotnet \
    && rm -rf /bin/sh
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.16-amd64 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Domain/Domain.csproj"  --configfile "./nuget.config"
RUN dotnet restore "Infrastructure/Infrastructure.csproj"  --configfile "./nuget.config"
RUN dotnet restore "Api/Api.csproj"  --configfile "./nuget.config"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER nobody
ENTRYPOINT ["dotnet", "Api.dll"]
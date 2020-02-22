FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -o /out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine-arm64v8 AS runtime
WORKDIR /app
COPY --from=build ./out/ .
ENV ASPNETCORE_ENVIRONMENT = ${environment}
ENV ASPNETCORE_URLS http://+:6001
ENTRYPOINT ["dotnet", "TestApi.dll"]
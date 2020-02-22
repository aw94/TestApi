FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -o /out
RUN ls -al

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine-arm64v8 AS runtime
WORKDIR /app
COPY --from=build ./out/ .
RUN ls -al
ENV ASPNETCORE_ENVIRONMENT = ${environment}
ENV ASPNETCORE_URLS http://+:6001
ENTRYPOINT ["dotnet", "TestApi.dll"]

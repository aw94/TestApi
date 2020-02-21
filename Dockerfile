FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster-arm32v7 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -o /out
RUN ls -al

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim-arm32v7 AS runtime
COPY --from=build ./out/ .
ENV ASPNETCORE_ENVIRONMENT = ${environment}
ENV ASPNETCORE_URLS http://+:6001
ENTRYPOINT ["dotnet", "TestApi.dll"]

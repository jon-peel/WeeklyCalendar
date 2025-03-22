FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
COPY docker-run.sh .
RUN chmod +x docker-run.sh

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["/app/docker-run.sh"]

#Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /ShortUrl.App
COPY ShortUrl.Api ./ShortUrl.Api
COPY ShortUrl.Core ./ShortUrl.Core
COPY ShortUrl.Entities ./ShortUrl.Entities
COPY ShortUrl.Repository ./ShortUrl.Repository
COPY ShortUrl.sln ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /ShortUrl.App
COPY --from=build /ShortUrl.App/out .
ENV ASPNETCORE_URLS=http://*80
CMD dotnet ShortUrl.Api.dll
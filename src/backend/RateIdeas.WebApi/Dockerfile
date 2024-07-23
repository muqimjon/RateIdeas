FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./src/backend/RateIdeas.WebApi/RateIdeas.WebApi.csproj ./src/backend/RateIdeas.WebApi/
RUN dotnet restore ./src/backend/RateIdeas.WebApi/RateIdeas.WebApi.csproj

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "RateIdeas.WebApi.dll"]

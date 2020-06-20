FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Restore solution
COPY ./CloudTrader.Api.sln ./
COPY ./src/CloudTrader.Api/CloudTrader.Api.csproj  ./src/CloudTrader.Api/CloudTrader.Api.csproj
COPY ./src/CloudTrader.Api.Data/CloudTrader.Api.Data.csproj  ./src/CloudTrader.Api.Data/CloudTrader.Api.Data.csproj
COPY ./src/CloudTrader.Api.Service/CloudTrader.Api.Service.csproj  ./src/CloudTrader.Api.Service/CloudTrader.Api.Service.csproj
COPY ./test/CloudTrader.Api.Tests/CloudTrader.Api.Tests.csproj  ./test/CloudTrader.Api.Tests/CloudTrader.Api.Tests.csproj
COPY ./test/CloudTrader.Api.Service.Tests/CloudTrader.Api.Service.Tests.csproj  ./test/CloudTrader.Api.Service.Tests/CloudTrader.Api.Service.Tests.csproj

RUN dotnet restore

# Copy everything else and build
COPY . ./

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "CloudTrader.Api.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Restore solution
COPY ./CloudTrader.Api.sln ./
COPY ./CloudTrader.Api/CloudTrader.Api.csproj  ./CloudTrader.Api/CloudTrader.Api.csproj
COPY ./CloudTrader.Users.Data/CloudTrader.Users.Data.csproj  ./CloudTrader.Users.Data/CloudTrader.Users.Data.csproj
COPY ./CloudTrader.Users.Domain/CloudTrader.Users.Domain.csproj  ./CloudTrader.Users.Domain/CloudTrader.Users.Domain.csproj
COPY ./CloudTrader.Api.Tests/CloudTrader.Api.Tests.csproj  ./CloudTrader.Api.Tests/CloudTrader.Api.Tests.csproj
COPY ./CloudTrader.Users.Domain.Tests/CloudTrader.Users.Domain.Tests.csproj  ./CloudTrader.Users.Domain.Tests/CloudTrader.Users.Domain.Tests.csproj

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

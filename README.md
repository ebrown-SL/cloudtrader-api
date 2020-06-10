# cloudtrader-api

External-facing gateway API for the CloudTrader project.

## Running locally with Visual Studio

Open the project from the solution file, and use the run button, and access the api at `https://localhost:5000`

## Running unit tests

Run `dotnet test`

## Running with docker

Build the image

`docker build . -t cloudtrader-api:latest`

Start the container

`docker run -p 5000:80 cloudtrader-api:latest`

Access the api at `http://localhost:5000`

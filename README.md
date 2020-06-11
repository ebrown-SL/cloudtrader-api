# cloudtrader-api

External-facing gateway API for the CloudTrader project.

## Running locally with Visual Studio

Open the project from the solution file, run, and access the api at `https://localhost:5001`.

In development mode the `JWT_KEY` variable should be set in the user secrets which will be used to generate and verify JWT tokens. The key can be anything but will fail if less than 16 characters.

`dotnet user-secrets set "JWT_KEY" "<your-key>"`

## Running unit tests

Run `dotnet test`

## Running with docker

Build the image

`docker build . -t cloudtrader-api:latest`

Start the container. You need to set the `JWT_KEY` as an environment variable when running the container.

`docker run -p 5000:80 -e JWT_KEY=<your-key> cloudtrader-api:latest`

Access the api at `http://localhost:5000`

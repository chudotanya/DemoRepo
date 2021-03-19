# https://hub.docker.com/_/microsoft-dotnet
# FROM stitch:latest
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source

COPY GitFargateDemo.sln ./
COPY . ./GitFargateDemo

WORKDIR /source/GitFargateDemo
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT dotnet GitFargateDemo.dll; sleep 20
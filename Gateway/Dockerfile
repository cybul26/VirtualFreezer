FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /app
COPY . .
RUN dotnet publish ./Gateway/src/VirtualFreezer.Gateway/VirtualFreezer.Gateway.csproj -o /app/published-app -c Release -r linux-arm64

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/published-app /app


ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT [ "dotnet", "/app/VirtualFreezer.Gateway.dll" ]
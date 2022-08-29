FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /app
COPY . .
RUN dotnet publish ./AccountVerification/src/VirtualFreezer.AccountVerification/VirtualFreezer.AccountVerification.csproj -o /app/published-app -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/published-app /app


ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT [ "dotnet", "/app/VirtualFreezer.AccountVerification.dll" ]
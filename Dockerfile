FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /maritime-cargo-aspnet

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /maritime-cargo-aspnet
COPY --from=build-env /maritime-cargo-aspnet/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Web.dll"]
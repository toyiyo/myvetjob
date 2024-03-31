FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app
COPY . .

# install libman library
RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
ENV PATH="$PATH:/root/.dotnet/tools"

# restore dependencies
WORKDIR /app/myvetjob.Web.Mvc
RUN dotnet restore
RUN libman restore

# Build and publish a release
WORKDIR /app
RUN dotnet publish myvetjob.Web.Mvc/myvetjob.Web.Mvc.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "myvetjob.Web.Mvc.dll"]
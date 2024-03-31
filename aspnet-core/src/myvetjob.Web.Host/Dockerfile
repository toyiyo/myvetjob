FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/myvetjob.Web.Host/myvetjob.Web.Host.csproj", "src/myvetjob.Web.Host/"]
COPY ["src/myvetjob.Web.Core/myvetjob.Web.Core.csproj", "src/myvetjob.Web.Core/"]
COPY ["src/myvetjob.Application/myvetjob.Application.csproj", "src/myvetjob.Application/"]
COPY ["src/myvetjob.Core/myvetjob.Core.csproj", "src/myvetjob.Core/"]
COPY ["src/myvetjob.EntityFrameworkCore/myvetjob.EntityFrameworkCore.csproj", "src/myvetjob.EntityFrameworkCore/"]
WORKDIR "/src/src/myvetjob.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/myvetjob.Web.Host", "src/myvetjob.Web.Host"]
COPY ["src/myvetjob.Web.Core", "src/myvetjob.Web.Core"]
COPY ["src/myvetjob.Application", "src/myvetjob.Application"]
COPY ["src/myvetjob.Core", "src/myvetjob.Core"]
COPY ["src/myvetjob.EntityFrameworkCore", "src/myvetjob.EntityFrameworkCore"]
WORKDIR "/src/src/myvetjob.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "myvetjob.Web.Host.dll"]

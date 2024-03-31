FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/myvetjob.Web.Mvc/myvetjob.Web.Mvc.csproj", "src/myvetjob.Web.Mvc/"]
COPY ["src/myvetjob.Web.Core/myvetjob.Web.Core.csproj", "src/myvetjob.Web.Core/"]
COPY ["src/myvetjob.Application/myvetjob.Application.csproj", "src/myvetjob.Application/"]
COPY ["src/myvetjob.Core/myvetjob.Core.csproj", "src/myvetjob.Core/"]
COPY ["src/myvetjob.EntityFrameworkCore/myvetjob.EntityFrameworkCore.csproj", "src/myvetjob.EntityFrameworkCore/"]
WORKDIR "/src/src/myvetjob.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/myvetjob.Web.Mvc", "src/myvetjob.Web.Mvc"]
COPY ["src/myvetjob.Web.Core", "src/myvetjob.Web.Core"]
COPY ["src/myvetjob.Application", "src/myvetjob.Application"]
COPY ["src/myvetjob.Core", "src/myvetjob.Core"]
COPY ["src/myvetjob.EntityFrameworkCore", "src/myvetjob.EntityFrameworkCore"]
WORKDIR "/src/src/myvetjob.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "myvetjob.Web.Mvc.dll"]

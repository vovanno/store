FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build

WORKDIR /app
COPY DAL/DAL.csproj ./DAL/
COPY BLL/BLL.csproj ./BLL/
COPY WebApi/WebApi.csproj ./WebApi/
COPY CrossCuttingFunctionality/CrossCuttingFunctionality.csproj ./CrossCuttingFunctionality/
COPY OnlineStore.sln .

RUN dotnet restore
COPY . .

ARG aspnetcore_environment="Local"
RUN export ASPNETCORE_ENVIRONMENT=aspnetcore_environment

RUN dotnet publish -o /publish
WORKDIR /app
RUN dotnet publish -o /publish
WORKDIR /publish
ENTRYPOINT ["dotnet", "WebApi.dll"]
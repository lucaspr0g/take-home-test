FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["*.sln", "./"]
COPY ["Fundo.Applications.UseCases/Fundo.Applications.UseCases.csproj", "Fundo.Applications.UseCases/"]
COPY ["Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj", "Fundo.Applications.WebApi/"]
COPY ["Fundo.Core/Fundo.Core.csproj", "Fundo.Core/"]
COPY ["Fundo.Infrastructure.Data/Fundo.Infrastructure.Data.csproj", "Fundo.Infrastructure.Data/"]
RUN dotnet restore "Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj"
COPY . .
RUN dotnet build "Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Fundo.Applications.WebApi.dll"]
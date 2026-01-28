FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FocusFlow.Domain/FocusFlow.Domain.csproj FocusFlow.Domain/
COPY FocusFlow.Application/FocusFlow.Application.csproj FocusFlow.Application/
COPY FocusFlow.Infrastructure/FocusFlow.Infrastructure.csproj FocusFlow.Infrastructure/
COPY FocusFlow.API/FocusFlow.API.csproj FocusFlow.API/

RUN dotnet restore FocusFlow.API/FocusFlow.API.csproj

COPY FocusFlow.Domain/ FocusFlow.Domain/
COPY FocusFlow.Application/ FocusFlow.Application/
COPY FocusFlow.Infrastructure/ FocusFlow.Infrastructure/
COPY FocusFlow.API/ FocusFlow.API/

WORKDIR /src/FocusFlow.API
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FocusFlow.API.dll"]

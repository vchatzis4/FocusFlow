FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FocusFlow.Web/FocusFlow.Web.csproj FocusFlow.Web/
RUN dotnet restore FocusFlow.Web/FocusFlow.Web.csproj

COPY FocusFlow.Web/ FocusFlow.Web/
WORKDIR /src/FocusFlow.Web
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html
COPY FocusFlow.Web/nginx.conf /etc/nginx/nginx.conf
COPY FocusFlow.Web/wwwroot/appsettings.Docker.json /usr/share/nginx/html/appsettings.json
EXPOSE 80

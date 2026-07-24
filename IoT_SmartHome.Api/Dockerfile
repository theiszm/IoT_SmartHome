FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine
WORKDIR /app

EXPOSE 5279

ENV ASPNETCORE_URLS=http://+:5279
ENV ASPNETCORE_ENVIRONMENT=Development

COPY IoT_SmartHome.Api.csproj ./
RUN dotnet restore "IoT_SmartHome.Api.csproj" --disable-parallel

COPY . .

ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile"]
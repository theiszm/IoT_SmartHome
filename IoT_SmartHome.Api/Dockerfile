FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine
WORKDIR /app

EXPOSE 7016
EXPOSE 5241

ENV ASPNETCORE_URLS=https://+:7016;http://+:5241
ENV ASPNETCORE_ENVIRONMENT=Development

COPY IoT_SmartHome.Api.csproj ./
RUN dotnet restore "IoT_SmartHome.Api.csproj" --disable-parallel

COPY . .

ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile"]
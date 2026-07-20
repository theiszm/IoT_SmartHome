FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

EXPOSE 7016
EXPOSE 5241

ENV ASPNETCORE_URLS=https://+:7016;http://+:5241
ENV ASPNETCORE_ENVIRONMENT=Development

COPY *.csproj ./
RUN dotnet restore

COPY . .

# 'dotnet watch' keeps listening for live edits in your C# files
ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile"]
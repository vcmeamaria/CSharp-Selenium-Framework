FROM mcr.microsoft.com/dotnet/sdk:8.0 AS test

WORKDIR /app

COPY SauceDemo.Automation.csproj ./

RUN dotnet restore SauceDemo.Automation.csproj

COPY . .

CMD ["dotnet", "test", "SauceDemo.Automation.csproj", "--no-restore", "--verbosity", "normal"]
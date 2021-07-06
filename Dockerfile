FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#Install nodejs, npm and chromium to use at runtime
RUN apt-get update
RUN apt-get install -y curl wget gnupg ca-certificates procps libxss1
RUN curl -sL https://deb.nodesource.com/setup_4.x | bash
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - 
RUN sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
RUN apt-get update
RUN apt-get install -y google-chrome-stable nodejs npm

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
#Install nodejs, npm and chromium to use on tests
RUN apt-get update
RUN apt-get install -y curl wget gnupg ca-certificates procps libxss1
RUN curl -sL https://deb.nodesource.com/setup_4.x | bash
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - 
RUN sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
RUN apt-get update
RUN apt-get install -y google-chrome-stable nodejs npm
#Build main application
WORKDIR /src
COPY ThFnsc.NFe.sln .
COPY src/ThFnsc.NFe/ThFnsc.NFe.csproj src/ThFnsc.NFe/
COPY src/ThFnsc.NFe.Core/ThFnsc.NFe.Core.csproj src/ThFnsc.NFe.Core/
COPY src/ThFnsc.NFe.Data/ThFnsc.NFe.Data.csproj src/ThFnsc.NFe.Data/
COPY src/ThFnsc.NFe.Infra/ThFnsc.NFe.Infra.csproj src/ThFnsc.NFe.Infra/
COPY tests/ThFnsc.NFe.Tests/ThFnsc.NFe.Tests.csproj tests/ThFnsc.NFe.Tests/
RUN dotnet restore
COPY . .
RUN dotnet build --no-restore -c Release
RUN dotnet test --no-build -c Release
RUN dotnet publish --no-build -c Release -o /app/publish

FROM base AS final
#Get everything
EXPOSE 80
WORKDIR /app
COPY --from=publish /app/publish/runtimes ./runtimes
COPY --from=publish /app/publish/InstrumentationEngine ./InstrumentationEngine
COPY --from=publish /app/publish/CodeCoverage ./CodeCoverage
COPY --from=publish /app/publish/?? .
COPY --from=publish /app/publish/??-??* .
COPY --from=publish /app/publish/[^ThFnsc.NFe]*.dll .
COPY --from=publish /app/publish/wwwroot ./wwwroot
COPY --from=publish /app/publish .
HEALTHCHECK --interval=5s --timeout=1s CMD wget --no-verbose --tries=1 --spider http://localhost/ || exit 1
ENTRYPOINT dotnet ThFnsc.NFe.dll
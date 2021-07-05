FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
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
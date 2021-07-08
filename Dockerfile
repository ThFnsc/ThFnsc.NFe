ARG DISTRO=ubuntu
ARG DISTRO_VERSION=21.04
ARG NODE_VERSION=14
ARG DOTNET_VERSION=5.0

#~~~~~~~~~~~~~~~~~~~~~~~~ base stage ~~~~~~~~~~~~~~~~~~~~~~~~#
FROM $DISTRO:$DISTRO_VERSION AS base
ARG DISTRO
ARG DISTRO_VERSION
ARG NODE_VERSION

#Basic packages
RUN apt-get update
RUN apt-get install -y curl wget gnupg ca-certificates procps libxss1 apt-transport-https

#Dotnet
RUN wget https://packages.microsoft.com/config/${DISTRO}/${DISTRO_VERSION}/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
	&& dpkg -i packages-microsoft-prod.deb \
	&& rm packages-microsoft-prod.deb

#Chromium
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - 
RUN sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'

#NodeJS
RUN curl -sL https://deb.nodesource.com/setup_$NODE_VERSION.x | bash -

#Install chromium and nodejs
RUN apt-get update
RUN apt-get install -y google-chrome-stable nodejs


#~~~~~~~~~~~~~~~~~~~~~~~~ build stage ~~~~~~~~~~~~~~~~~~~~~~~~#
FROM base AS build

#Install dotnet sdk
ARG DOTNET_VERSION
RUN apt-get install -y dotnet-sdk-$DOTNET_VERSION

#Copy files for restore
WORKDIR /src
COPY ThFnsc.NFe.sln .
COPY src/ThFnsc.NFe/ThFnsc.NFe.csproj src/ThFnsc.NFe/
COPY src/ThFnsc.NFe.Core/ThFnsc.NFe.Core.csproj src/ThFnsc.NFe.Core/
COPY src/ThFnsc.NFe.Data/ThFnsc.NFe.Data.csproj src/ThFnsc.NFe.Data/
COPY src/ThFnsc.NFe.Infra/ThFnsc.NFe.Infra.csproj src/ThFnsc.NFe.Infra/
COPY tests/ThFnsc.NFe.Tests/ThFnsc.NFe.Tests.csproj tests/ThFnsc.NFe.Tests/

#Restore
RUN dotnet restore

#Copy the rest of the files
COPY . .

#Build the application
RUN dotnet build --no-restore -c Release


#~~~~~~~~~~~~~~~~~~~~~~~~ test stage ~~~~~~~~~~~~~~~~~~~~~~~~#
FROM build AS test

#Tests the application
RUN dotnet test --no-build -c Release -r /src/TestResults


#~~~~~~~~~~~~~~~~~~~~~~~~ publish stage ~~~~~~~~~~~~~~~~~~~~~~~~#
FROM build as publish

#Publishes the application
RUN dotnet publish --no-build -c Release -o /app/publish


#~~~~~~~~~~~~~~~~~~~~~~~~ final stage ~~~~~~~~~~~~~~~~~~~~~~~~#
FROM base AS final

#Install aspnet runtime
ARG DOTNET_VERSION
RUN apt-get install -y aspnetcore-runtime-$DOTNET_VERSION

#Basic configs
ENV ASPNETCORE_URLS=http://*:80
EXPOSE 80
WORKDIR /app
HEALTHCHECK --interval=5s --timeout=1s CMD wget --no-verbose --tries=1 --spider http://localhost/ || exit 1

#Copy build files that rarely change
COPY --from=publish /app/publish/runtimes ./runtimes
COPY --from=publish /app/publish/InstrumentationEngine ./InstrumentationEngine
COPY --from=publish /app/publish/CodeCoverage ./CodeCoverage
COPY --from=publish /app/publish/?? ./
COPY --from=publish /app/publish/??-??* ./
COPY --from=publish /app/publish/wwwroot ./wwwroot

#Copy the important stuff
COPY --from=publish /app/publish/[^ThFnsc.NFe]*.dll ./
COPY --from=publish /app/publish ./
COPY --from=test /src/TestResults ./

ENTRYPOINT dotnet ThFnsc.NFe.dll
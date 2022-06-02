ARG DISTRO=ubuntu
ARG DISTRO_VERSION=22.04
ARG DOTNET_VERSION=6.0


#~~~~~~~~~~~~~~~~~~~~~~~~ base stage ~~~~~~~~~~~~~~~~~~~~~~~~#
FROM $DISTRO:$DISTRO_VERSION AS base
ARG DISTRO
ARG DISTRO_VERSION

#Basic packages
RUN apt-get update
RUN apt-get install -y wget gnupg ca-certificates procps libxss1 apt-transport-https

#Dotnet
RUN wget https://packages.microsoft.com/config/${DISTRO}/${DISTRO_VERSION}/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
	&& dpkg -i packages-microsoft-prod.deb \
	&& rm packages-microsoft-prod.deb

#Chromium
ARG DEBIAN_FRONTEND=noninteractive
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - 
RUN sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'

#Install chromium
RUN apt-get update
RUN apt-get install -y google-chrome-stable


#~~~~~~~~~~~~~~~~~~~~~~~~ restore files extraction stage ~~~~~~~~~~~~~~~~~~~~~~~~#
FROM $DISTRO:$DISTRO_VERSION AS restore
WORKDIR src

#Copy all project files
COPY . .

#Make a tarball with only the .csproj and .sln files
RUN find . \( -name *.csproj -or -name *.sln \) -print0 | tar -cvf restore.tar --null -T -

#Extract the tarball into /restore
RUN mkdir /restore
RUN tar -xvf restore.tar -C /restore

FROM base AS runtime
#Install aspnet runtime
ARG DOTNET_VERSION
RUN apt-get install -y aspnetcore-runtime-$DOTNET_VERSION


#~~~~~~~~~~~~~~~~~~~~~~~~ build stage ~~~~~~~~~~~~~~~~~~~~~~~~#
FROM runtime AS build

#Install dotnet sdk
ARG DOTNET_VERSION
RUN apt-get install -y dotnet-sdk-$DOTNET_VERSION

#Copy files for restore
WORKDIR /src

#Copy only the .csproj and .sln files
COPY --from=restore /restore ./

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
FROM runtime AS final

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
COPY --from=publish /app/publish .
COPY --from=test /src/TestResults .

ENTRYPOINT dotnet ThFnsc.NFe.dll
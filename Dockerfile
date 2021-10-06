FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
RUN ls -la 
COPY / ./
RUN ls -la 
RUN dotnet publish PageRendrAPI.sln -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0.6
WORKDIR /app
COPY --from=build-env /app/out .
ADD https://chromedriver.storage.googleapis.com/93.0.4577.63/chromedriver_linux64.zip chromedriver.zip
RUN apt-get update
RUN apt-get install unzip -y
RUN apt-get install wget -y
# install manually all the missing libraries
RUN apt-get install -y gconf-service libasound2 libatk1.0-0 libcairo2 libcups2 libfontconfig1 libgdk-pixbuf2.0-0 libgtk-3-0 libnspr4 libpango-1.0-0 libxss1 fonts-liberation libappindicator1 libnss3 lsb-release xdg-utils libcurl3-gnutls libcurl3-nss libcurl4 libgbm1 

# install chrome
RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
RUN dpkg -i google-chrome-stable_current_amd64.deb; apt-get -fy install
RUN unzip chromedriver.zip
RUN ls -la 
ENTRYPOINT ["dotnet", "PageRendrAPI.dll"]
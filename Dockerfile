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
ADD http://chromedriver.storage.googleapis.com/90.0.4430.24/chromedriver_linux64.zip chromedriver.zip
RUN sh -c "echo 'deb http://dl.google.com/linux/chrome/deb/ stable main' >>   /etc/apt/sources.list"
RUN apt-get update
RUN apt-get install unzip -y
RUN apt-get install -y google-chrome-stable
RUN unzip chromedriver.zip
RUN ls -la 
ENTRYPOINT ["dotnet", "PageRendrAPI.dll"]
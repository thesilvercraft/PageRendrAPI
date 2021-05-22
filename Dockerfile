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
ENTRYPOINT ["dotnet", "PageRendrAPI.dll"]
# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only necessary files for restore
COPY ProductManage.Api/*.csproj ./ProductManage.Api/
RUN dotnet restore ./ProductManage.Api/ProductManage.Api.csproj

# Copy the rest of the application files
COPY . ./

# Build and publish the application
RUN dotnet publish ./ProductManage.Api/ProductManage.Api.csproj -c Release -o /app/out

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Start the application
ENTRYPOINT ["dotnet", "ProductManage.Api.dll"]

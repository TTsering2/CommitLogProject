# Stage 1: Build the application and tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restore CommitLogApp project dependencies
COPY CommitLogApp/CommitLogApp.csproj CommitLogApp/
RUN dotnet restore CommitLogApp/CommitLogApp.csproj

# Copy and restore CommitLogApp.Tests project dependencies
COPY CommitLogApp.Tests/CommitLogApp.Tests.csproj CommitLogApp.Tests/
RUN dotnet restore CommitLogApp.Tests/CommitLogApp.Tests.csproj

# Copy full application and test source code
COPY . .

# Build CommitLogApp and CommitLogApp.Tests
RUN dotnet build CommitLogApp/CommitLogApp.csproj -c Release --no-restore
RUN dotnet build CommitLogApp.Tests/CommitLogApp.Tests.csproj -c Release --no-restore

# Stage 2: Run tests and generate coverage report
FROM build AS testrunner
WORKDIR /app/CommitLogApp.Tests
RUN dotnet test --no-build --logger:trx /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Stage 3: Publish application
FROM build AS publish
WORKDIR /app/CommitLogApp
RUN dotnet publish CommitLogApp.csproj -c Release -o /app/publish

# Stage 4: Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommitLogApp.dll"]

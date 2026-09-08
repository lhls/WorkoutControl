# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["WorkoutControl.Api/WorkoutControl.Api.csproj", "./"]
RUN dotnet restore "WorkoutControl.Api.csproj"

COPY . .
RUN dotnet build "WorkoutControl.Api/WorkoutControl.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WorkoutControl.Api/WorkoutControl.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

# Ensure Kestrel listens on the mapped container port
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 8080
EXPOSE 4024
EXPOSE 4711
ENTRYPOINT ["dotnet", "WorkoutControl.Api.dll"]

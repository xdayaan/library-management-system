# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Use SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LMS.csproj", "./"]
RUN dotnet restore "LMS.csproj"
COPY . .
RUN dotnet build "LMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LMS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LMS.dll"]

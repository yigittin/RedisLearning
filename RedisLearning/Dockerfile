#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RedisLearning/RedisLearning.csproj", "RedisLearning/"]
RUN dotnet restore "RedisLearning/RedisLearning.csproj"
COPY . .
WORKDIR "/src/RedisLearning"
RUN dotnet build "RedisLearning.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedisLearning.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisLearning.dll"]
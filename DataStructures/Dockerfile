FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DataStructures/DataStructures.csproj", "DataStructures/"]
RUN dotnet restore "DataStructures/DataStructures.csproj"
COPY . .
WORKDIR "/src/DataStructures"
RUN dotnet build "DataStructures.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataStructures.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataStructures.dll"]

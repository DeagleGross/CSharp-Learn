FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Sorting/Sorting.csproj", "Sorting/"]
RUN dotnet restore "Sorting/Sorting.csproj"
COPY . .
WORKDIR "/src/Sorting"
RUN dotnet build "Sorting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sorting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sorting.dll"]

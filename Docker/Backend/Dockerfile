FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY ../TestAPI/ .
RUN dotnet restore "TestAPI.csproj"
RUN dotnet build "TestAPI.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish TestAPI.csproj -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet" , "TestAPI.dll"]
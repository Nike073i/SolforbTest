FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY Core/SolforbTest.Application/*.csproj Core/SolforbTest.Application/
COPY Core/SolforbTest.Domain/*.csproj Core/SolforbTest.Domain/
COPY Data/SolforbTest.EfContext/*.csproj Data/SolforbTest.EfContext/
COPY Data/SolforbTest.EfContext.SqlServer/*.csproj Data/SolforbTest.EfContext.SqlServer/
COPY Presentation/SolforbTest.WebClient/*.csproj Presentation/SolforbTest.WebClient/

RUN dotnet restore Presentation/SolforbTest.WebClient/SolforbTest.WebClient.csproj

COPY . .
RUN dotnet publish --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SolforbTest.WebClient.dll"]
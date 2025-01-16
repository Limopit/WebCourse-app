FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ./APIGateway/CourseApp_ApiGateway/*.sln ./
COPY ./APIGateway/CourseApp_ApiGateway/CourseApp_ApiGateway/*.csproj CourseApp_ApiGateway/

RUN dotnet restore CourseApp_ApiGateway/CourseApp_ApiGateway.csproj

COPY ./APIGateway/CourseApp_ApiGateway/ /src/

WORKDIR /src/CourseApp_ApiGateway

RUN dotnet publish -c Release -o /app/publish 

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

COPY ./certs /https:ro

EXPOSE 5003

ENTRYPOINT ["dotnet", "CourseApp_ApiGateway.dll"]

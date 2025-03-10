FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Backend/CourseAppNotificationService/*.sln ./

COPY Backend/CourseAppNotificationService/CourseAppNotificationService_Domain/*.csproj CourseAppNotificationService_Domain/
COPY Backend/CourseAppNotificationService/CourseAppNotificationService_Infrastructure/*.csproj CourseAppNotificationService_Infrastructure/
COPY Backend/CourseAppNotificationService/CourseAppNotificationService_WebAPI/*.csproj CourseAppNotificationService_WebAPI/

RUN dotnet restore "CourseAppNotificationService_WebAPI/CourseAppNotificationService_WebAPI.csproj"

COPY Backend/CourseAppNotificationService/ .

WORKDIR /src/CourseAppNotificationService_WebAPI

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

COPY ./certs /https

RUN cp /https/cert.crt /usr/local/share/ca-certificates/cert.crt && update-ca-certificates

EXPOSE 5004

ENTRYPOINT ["dotnet", "CourseAppNotificationService_WebAPI.dll"]

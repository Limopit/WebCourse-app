FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Backend/CourseAppCourseService/*.sln ./

COPY Backend/CourseAppCourseService/CourseAppCourseService_Application/*.csproj CourseAppCourseService_Application/
COPY Backend/CourseAppCourseService/CourseAppCourseService_Domain/*.csproj CourseAppCourseService_Domain/
COPY Backend/CourseAppCourseService/CourseAppCourseService_Infrastructure/*.csproj CourseAppCourseService_Infrastructure/
COPY Backend/CourseAppCourseService/CourseAppCourseService_WebAPI/*.csproj CourseAppCourseService_WebAPI/

RUN dotnet restore "CourseAppCourseService_WebAPI/CourseAppCourseService_WebAPI.csproj"

COPY Backend/CourseAppCourseService/ .

WORKDIR /src/CourseAppCourseService_WebAPI

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

COPY ./certs /https

RUN cp /https/cert.crt /usr/local/share/ca-certificates/cert.crt && update-ca-certificates

EXPOSE 5000

ENTRYPOINT ["dotnet", "CourseAppCourseService_WebAPI.dll"]

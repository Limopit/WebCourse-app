FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем только нужные .sln и .csproj файлы для рабочих проектов
COPY ./Backend/CourseAppUserService/*.sln ./
COPY ./Backend/CourseAppUserService/CourseAppUserService_Application/*.csproj CourseAppUserService_Application/
COPY ./Backend/CourseAppUserService/CourseAppUserService_Domain/*.csproj CourseAppUserService_Domain/
COPY ./Backend/CourseAppUserService/CourseAppUserService_Infrastructure/*.csproj CourseAppUserService_Infrastructure/
COPY ./Backend/CourseAppUserService/CourseAppUserService_IdentityServer/*.csproj CourseAppUserService_IdentityServer/
COPY ./Backend/CourseAppUserService/CourseAppUserService_WebAPI/*.csproj CourseAppUserService_WebAPI/
COPY ./Backend/CourseAppUserService/CourseAppUserService_IdentityServer/appsettings.Identity.json /app/

# Восстанавливаем зависимости, исключая тестовые проекты
RUN dotnet restore CourseAppUserService_WebAPI/CourseAppUserService_WebAPI.csproj

# Копируем все остальные файлы, но исключаем тестовые проекты
COPY ./Backend/CourseAppUserService/ /src/

WORKDIR /src/CourseAppUserService_WebAPI

# Публикуем приложение
RUN dotnet publish -c Release -o /app/publish 

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

RUN apt-get update && \
    apt-get install -y curl netcat-traditional telnet && \
    apt-get clean 

COPY ./certs /https:ro

EXPOSE 5002

ENTRYPOINT ["dotnet", "CourseAppUserService_WebAPI.dll"]

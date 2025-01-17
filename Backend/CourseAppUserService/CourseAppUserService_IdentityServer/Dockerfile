FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Backend/CourseAppUserService/CourseAppUserService_IdentityServer/*.csproj CourseAppUserService_IdentityServer/
RUN dotnet restore "CourseAppUserService_IdentityServer/CourseAppUserService_IdentityServer.csproj"

COPY Backend/CourseAppUserService/CourseAppUserService_IdentityServer/ /src/CourseAppUserService_IdentityServer/

WORKDIR /src/CourseAppUserService_IdentityServer
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

COPY ./certs /https

RUN cp /https/cert.crt /usr/local/share/ca-certificates/cert.crt && update-ca-certificates

EXPOSE 5001

ENTRYPOINT ["dotnet", "CourseAppUserService_IdentityServer.dll"]

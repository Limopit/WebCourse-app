FROM mcr.microsoft.com/mssql/server:2022-latest

USER root
RUN apt-get update && \
    apt-get install -y curl netcat-traditional telnet && \
    apt-get clean

USER mssql

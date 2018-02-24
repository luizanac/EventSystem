FROM microsoft/dotnet
COPY . /app
WORKDIR /app

RUN dotnet publish -c Release -o /app/deploy

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://*:5000

WORKDIR /app/deploy

EXPOSE 5000/tcp

ENTRYPOINT dotnet EventSystem.Api.dll
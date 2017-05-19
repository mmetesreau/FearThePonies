FROM microsoft/dotnet

WORKDIR /app

COPY ./FearServer .

ENV APP_URL  http://+:8080

EXPOSE 8080

RUN dotnet restore

ENTRYPOINT ["dotnet", "run"]

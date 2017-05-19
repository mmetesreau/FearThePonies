FROM microsoft/dotnet

WORKDIR /app

COPY ./FearServer .

ENV APP_URL  http://+:5000

EXPOSE 5000

RUN dotnet restore

ENTRYPOINT ["dotnet", "run -- --duration 60"]

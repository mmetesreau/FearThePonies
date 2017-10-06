FROM microsoft/dotnet:1.1-sdk

WORKDIR /app

COPY ./FearServer .

EXPOSE 8080

RUN dotnet restore

CMD ["dotnet", "run"]

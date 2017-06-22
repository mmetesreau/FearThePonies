FROM microsoft/dotnet

WORKDIR /app

COPY ./FearServer .

EXPOSE 8080

RUN dotnet restore

ENTRYPOINT ["dotnet", "run"]

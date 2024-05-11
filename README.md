# Fear the ponies II - Code dojo about cryptography basics 

## Breaking news: We are under attack!

After their heavy defeat during the previous battle, ponies are back and are still aiming at taking over the world and finally subjugate humanity. Their new battle plan is about to start and our utmost fear may become reality, the ponies might have gathered new allies. 

Being one of the brave warriors who saved the world during the last epic battle or being a new fighter, we rely on you. 

## Technical Instructions:

### Requirements
- .NET 8 or Docker

### Install & Run

```
cd src
dotnet restore
dotnet run
```

Go to ```http://localhost:5000```

By default the code dojo takes 90 minutes but you can specify the duration in minutes:

```
dotnet run -- --duration 60
```

or

```
$env:FTP_DURATION="120"
dotnet run
```

You can also specify the db path:

```
dotnet run -- --dbpath ./
```

Finally, to access the leaderboard, refer to the following URL ```http://localhost:5000/leaderboard```.

If you prefer, you can use docker instead:

```
docker build -t feartheponies ./
docker run -p 5000:8080 -e TZ=Europe/Paris -v .\:/app/data feartheponies --duration 60 --dbpath /app/data
```

## Special thanks:

Based on Raphaël Wach ([@raphaelwach](https://twitter.com/raphaelwach)) amazing code dojo

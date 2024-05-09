# Fear the ponies - Code dojo about cryptography basics 

## Breaking news: We are under attack!

Ponies just forged a secret alliance in order to end human civilization and their first move should happen in a couple of hours. We require your help to access their information which should allow us to arrange a strike back plan or at least buy us time.

Start your mission and don't fail. You're our only hope...

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

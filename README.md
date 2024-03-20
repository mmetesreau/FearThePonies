# Fear the ponies - Code dojo about cryptography basics 

## Breaking news: We are under attack!

Ponies just forged a secret alliance in order to end human civilization and their first move should happen in a couple of hours. We require your help to access their informations which should allow us to arrange a strike back plan or at least buy us time.

Start your mission and don't fail. You're our only hope...

## Technical Instructions:

### Requirements
- .NET 8

### Install & Run

```
cd src
dotnet restore
dotnet run
```

Go to ```http://localhost:5000```

By default the code dojo take 90 minutes but you can specify the duration in minutes:

```
dotnet run -- --duration 60
```

or

```
$env:DURATION="120"
dotnet run
```

## Special thanks:

Based on Raphaël Wach ([@raphaelwach](https://twitter.com/raphaelwach)) amazing code dojo

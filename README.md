# Fear the ponies - Code dojo about cryptography basics 

## Breaking news: We are under attack!

Ponies just forged a secret alliance in order to end human civilization and their first move should happen in less than 1 hour. We require your help to access their informations which should allow us to arrange a strike back plan or at least buy us time.

Start your mission and don't fail. You're our only hope...

## The algorithms used:

* Caesar cipher
* Monoalphabetic substitution cipher
* Vigenere cipher

## Technical Instructions:

### Requirements
- NET Core 1.1 SDK

### Install & Run

```
dotnet restore
dotnet run
```

Go to ```http://localhost:8080```

By default the code dojo take 1 hour but you can specify the ending date:

```
dotnet run -- --end "2017/06/22 12:30:00" 
```

or

```
$env:END="2017/06/22 12:30:00"
dotnet run -- --end "2017/06/22 12:30:00" 
```


## Special thanks:

Based on Raphaël Wach ([@raphaelwach](https://twitter.com/raphaelwach)) amazing code dojo

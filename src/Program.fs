open FearThePonies

module Domain =
    module Team =
        open System.Security.Cryptography
        let private hash (str: string) =
            use sha = SHA256.Create()
            str
            |> System.Text.Encoding.UTF8.GetBytes
            |> sha.ComputeHash
            |> System.Convert.ToBase64String

        type Id = string
        module Id =
            let generate teamName =
                if System.String.IsNullOrEmpty teamName
                then Error "This team name is invalid."
                else Ok (hash teamName)

        type Events =
        | TeamRegistered of TeamRegistered
        | ChallengeCompleted of ChallengeCompleted
        | WrongAnswerSubmitted of WrongAnswerSubmitted
        and TeamRegistered = {
            TeamName: string
            At: System.DateTime
        }
        and ChallengeCompleted = {
            Challenge: Challenge
            At: System.DateTime
        }
        and WrongAnswerSubmitted = {
            Challenge: Challenge
            At: System.DateTime
        }
        and Challenge =
            | FirstChallenge = 1
            | SecondChallenge = 2
            | ThirdChallenge = 3

        type Commands =
        | Register of Register
        | SubmitAnswerToFirstChallenge of SubmitAnswer
        | SubmitAnswerToSecondChallenge of SubmitAnswer
        | SubmitAnswerToThirdChallenge of SubmitAnswer
        and Register = {
            TeamName: string
            Now: System.DateTime
        }
        and SubmitAnswer = {
            Answer: string
            Now: System.DateTime
        }

        type Projection = {
            Registered: bool
            LastChallengeCompleted: Challenge option
        }

        let private apply projection = function
            | TeamRegistered _ -> { projection with Registered = true }
            | ChallengeCompleted evt -> { projection with LastChallengeCompleted = Some evt.Challenge }
            | _ -> projection

        let handle cmd history =
            let projection =
                history
                |> List.fold apply {
                    Registered = false
                    LastChallengeCompleted = None
                }

            match cmd, projection with
            | Register _, { Registered = true } -> Error "This team is already registered."
            | Register cmd, _ ->
                Ok [
                    TeamRegistered {
                        TeamName = cmd.TeamName
                        At = cmd.Now
                    }
                ]
            | _, { Registered = false } -> Error "This team is not yet registered."

            | SubmitAnswerToFirstChallenge _, { LastChallengeCompleted = Some challenge } when challenge >= Challenge.FirstChallenge -> Ok []
            | SubmitAnswerToFirstChallenge cmd, { LastChallengeCompleted = None } ->
                Ok [
                    if hash cmd.Answer = "KXIZ595CS7UsBA56LLvZAk968Y4oOJT+WcpqvAMTw8Q="
                    then ChallengeCompleted { At = cmd.Now; Challenge = Challenge.FirstChallenge }
                    else WrongAnswerSubmitted { At = cmd.Now; Challenge = Challenge.FirstChallenge }
                ]
            | SubmitAnswerToFirstChallenge _, _ -> Error "You need to complete previous challenges first."

            | SubmitAnswerToSecondChallenge _, { LastChallengeCompleted = Some challenge } when challenge >= Challenge.SecondChallenge -> Ok []
            | SubmitAnswerToSecondChallenge cmd, { LastChallengeCompleted = Some Challenge.FirstChallenge } ->
                Ok [
                    if hash cmd.Answer = "LZjgDIciRR66HUouBiyPmLp2pKQKNdovhkwZUxD8u+E="
                    then ChallengeCompleted { At = cmd.Now; Challenge = Challenge.SecondChallenge }
                    else WrongAnswerSubmitted { At = cmd.Now; Challenge = Challenge.SecondChallenge }
                ]
            | SubmitAnswerToSecondChallenge _, _ -> Error "You need to complete previous challenges first."

            | SubmitAnswerToThirdChallenge _, { LastChallengeCompleted = Some challenge } when challenge >= Challenge.ThirdChallenge -> Ok []
            | SubmitAnswerToThirdChallenge cmd, { LastChallengeCompleted = Some Challenge.SecondChallenge } ->
                Ok [
                    if hash cmd.Answer = "c0dctApWjo2ooEXO0RATfhWfiQrE2og7axfcZRs6gEk="
                    then ChallengeCompleted { At = cmd.Now; Challenge = Challenge.ThirdChallenge }
                    else WrongAnswerSubmitted { At = cmd.Now; Challenge = Challenge.ThirdChallenge }
                ]
            | SubmitAnswerToThirdChallenge _, _ -> Error "You need to complete previous challenges first."

module Services =
    type Infra = {
        EndOfTheWorld: string
        GetNow: unit -> System.DateTime
        GetEvents: Domain.Team.Id -> Result<Domain.Team.Events list, string>
        StoreEvents: Domain.Team.Id -> Domain.Team.Events list -> Result<unit, string>
    }

    let private handle infra teamId cmd =
        infra.GetEvents teamId
        |> Result.bind (Domain.Team.handle cmd)
        |> Result.bind (infra.StoreEvents teamId)

    let register infra (teamName: string) =
        let teamName = teamName.Trim()
        Domain.Team.Id.generate teamName
        |> Result.bind (fun teamId ->
            Domain.Team.Register {
                TeamName = teamName
                Now = infra.GetNow()
            }
            |> handle infra teamId
            |> Result.map (fun _ -> teamId))

    let submitAnswerToFirstChallenge infra teamId answer =
        Domain.Team.SubmitAnswerToFirstChallenge {
            Answer = answer
            Now = infra.GetNow()
        } |> handle infra teamId

    let submitAnswerToSecondChallenge infra teamId answer =
        Domain.Team.SubmitAnswerToSecondChallenge {
            Answer = answer
            Now = infra.GetNow()
        } |> handle infra teamId

    let submitAnswerToThirdChallenge infra teamId answer =
        Domain.Team.SubmitAnswerToThirdChallenge {
            Answer = answer
            Now = infra.GetNow()
        } |> handle infra teamId

module Readmodels =
    open System.Text.Json

    type Store = {
        Get: ReadmodelName -> ReadmodelId -> Data option
        GetAll: ReadmodelName -> Data list
        Save: ReadmodelName -> ReadmodelId -> Data -> unit
    }
    and ReadmodelName = string
    and ReadmodelId = string
    and Data = string

    [<Literal>]
    let private challengeStatus_readmodel = "challengeStatus_readmodel"

    [<Literal>]
    let private leaderboard_readmodel = "leaderboard_readmodel"

    module Queries =
        let private getChallengeStatus store teamId challenge =
            store.Get challengeStatus_readmodel $"{teamId}-{challenge}"
            |> Option.map JsonSerializer.Deserialize<Challenge.ChallengeStatus>
            |> Option.defaultValue { CompletedAt = None }

        let getFirstChallengeStatus store teamId =
            Domain.Team.Challenge.FirstChallenge
            |> getChallengeStatus store teamId

        let getSecondChallengeStatus store teamId =
            Domain.Team.Challenge.SecondChallenge
            |> getChallengeStatus store teamId

        let getThirdChallengeStatus store teamId =
            Domain.Team.Challenge.ThirdChallenge
            |> getChallengeStatus store teamId

        let getLeaderboard store =
            store.GetAll leaderboard_readmodel
            |> List.map JsonSerializer.Deserialize<Leaderboard.TeamStatus>

    let private handleChallengeStatus store teamId = function
        | Domain.Team.ChallengeCompleted evt ->
            ({ CompletedAt = Some evt.At }: Challenge.ChallengeStatus)
            |> JsonSerializer.Serialize
            |> store.Save challengeStatus_readmodel $"{teamId}-{evt.Challenge}"
        | _ -> ()

    let private handleLeaderboard store teamId event =
        let teamStatus =
            store.Get leaderboard_readmodel teamId
            |> Option.map JsonSerializer.Deserialize<Leaderboard.TeamStatus>
            |> Option.defaultValue ({
                TeamName = "-"
                FirstChallengeAttempts = 0
                FirstChallengeCompleted = false
                SecondChallengeAttempts = 0
                SecondChallengeCompleted = false
                ThirdChallengeAttempts = 0
                ThirdChallengeCompleted = false
            }: Leaderboard.TeamStatus)

        match event with
        | Domain.Team.TeamRegistered evt ->
            Some {
                teamStatus with TeamName = evt.TeamName
            }
        | Domain.Team.ChallengeCompleted { Challenge = Domain.Team.Challenge.FirstChallenge } ->
            Some {
                teamStatus with
                    FirstChallengeAttempts = teamStatus.FirstChallengeAttempts + 1
                    FirstChallengeCompleted = true
            }
        | Domain.Team.WrongAnswerSubmitted { Challenge = Domain.Team.Challenge.FirstChallenge } ->
            Some {
                teamStatus with FirstChallengeAttempts = teamStatus.FirstChallengeAttempts + 1
            }
        | Domain.Team.ChallengeCompleted { Challenge = Domain.Team.Challenge.SecondChallenge } ->
            Some {
                teamStatus with
                    SecondChallengeAttempts = teamStatus.SecondChallengeAttempts + 1
                    SecondChallengeCompleted =  true
            }
        | Domain.Team.WrongAnswerSubmitted { Challenge = Domain.Team.Challenge.SecondChallenge } ->
            Some {
                teamStatus with SecondChallengeAttempts = teamStatus.SecondChallengeAttempts + 1
            }
        | Domain.Team.ChallengeCompleted { Challenge = Domain.Team.Challenge.ThirdChallenge } ->
            Some {
                teamStatus with
                    ThirdChallengeAttempts = teamStatus.ThirdChallengeAttempts + 1
                    ThirdChallengeCompleted = true
            }
        | Domain.Team.WrongAnswerSubmitted { Challenge = Domain.Team.Challenge.ThirdChallenge } ->
            Some {
                teamStatus with ThirdChallengeAttempts = teamStatus.ThirdChallengeAttempts + 1
            }
        | _ -> None
        |> Option.map (JsonSerializer.Serialize >> (store.Save leaderboard_readmodel teamId))
        |> Option.defaultValue ()

    let handle store teamId =
        let apply event =
            handleChallengeStatus store teamId event
            handleLeaderboard store teamId event

        List.iter apply

module Queries =
    open Giraffe

    let welcome =
        Welcome.render ()
        |> htmlString

    let register =
        Register.render ()
        |> htmlString

    let firstChallenge endOfTheWorld store teamId =
        Readmodels.Queries.getFirstChallengeStatus store teamId
        |> fun status -> FirstChallenge.render endOfTheWorld teamId status.CompletedAt
        |> htmlString

    let secondChallenge endOfTheWorld store teamId =
        Readmodels.Queries.getSecondChallengeStatus store teamId
        |> fun status -> SecondChallenge.render endOfTheWorld teamId status.CompletedAt
        |> htmlString

    let thirdChallenge endOfTheWorld store teamId =
        Readmodels.Queries.getThirdChallengeStatus store teamId
        |> fun status -> ThirdChallenge.render endOfTheWorld teamId status.CompletedAt
        |> htmlString

    let leaderboard endOfTheWorld =
        Leaderboard.render endOfTheWorld
        |> htmlString

    let teamsStatus store =
        Readmodels.Queries.getLeaderboard store
        |> Leaderboard.renderTeamStatus
        |> htmlString

module Commands =
    open Microsoft.AspNetCore.Http
    open Giraffe

    [<CLIMutable>] type RegisterForm = { teamName: string }

    let register infra next (context: HttpContext) = task {
        let! form = context.BindFormAsync<RegisterForm>()

        return!
            Services.register infra form.teamName
            |> Result.map Register.renderSuccess
            |> Result.defaultWith Register.renderError
            |> fun page -> htmlString page next context
    }

    [<CLIMutable>] type SubmitAnwserForm = { answer: string }

    let submitAnswerToFirstChallenge store infra teamId next (context: HttpContext) = task {
        let! form = context.BindFormAsync<SubmitAnwserForm>()

        let result =
            Services.submitAnswerToFirstChallenge infra teamId form.answer
            |> Result.map (fun _ -> Queries.firstChallenge infra.EndOfTheWorld store teamId)
            |> Result.defaultWith failwith

        return! result next context
    }

    let submitAnswerToSecondChallenge store infra teamId next (context: HttpContext) = task {
        let! form = context.BindFormAsync<SubmitAnwserForm>()

        let result =
            Services.submitAnswerToSecondChallenge infra teamId form.answer
            |> Result.map (fun _ -> Queries.secondChallenge infra.EndOfTheWorld store teamId)
            |> Result.defaultWith failwith

        return! result next context
    }

    let submitAnswerToThirdChallenge store infra teamId next (context: HttpContext) = task {
        let! form = context.BindFormAsync<SubmitAnwserForm>()

        let result =
            Services.submitAnswerToThirdChallenge infra teamId form.answer
            |> Result.map (fun _ -> Queries.thirdChallenge infra.EndOfTheWorld store teamId)
            |> Result.defaultWith failwith

        return! result next context
    }

module Http =
    open Microsoft.AspNetCore.Http
    open Microsoft.Extensions.Logging
    open Giraffe
    open Giraffe.EndpointRouting

    let errorHandler (ex : System.Exception) (logger : ILogger) =
        logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
        clearResponse
        >=> setStatusCode StatusCodes.Status500InternalServerError
        >=> htmlString (ServerError.render ex.Message)

    let notFoundHandler =
        setStatusCode StatusCodes.Status404NotFound
        >=> htmlString (NotFound.render ())

    let endpoints store (infra: Services.Infra) = [
        GET [
            route "/" Queries.welcome
            route "/register" Queries.register
            routef "/challenge1/%s" (Queries.firstChallenge infra.EndOfTheWorld store)
            routef "/challenge2/%s" (Queries.secondChallenge infra.EndOfTheWorld store)
            routef "/challenge3/%s" (Queries.thirdChallenge infra.EndOfTheWorld store)
            route "/leaderboard" (Queries.leaderboard infra.EndOfTheWorld)
            route "/teams-status" (warbler (fun _ -> Queries.teamsStatus store))
        ]
        POST [
            route "/register" (Commands.register infra)
            routef "/challenge1/%s" (Commands.submitAnswerToFirstChallenge store infra)
            routef "/challenge2/%s" (Commands.submitAnswerToSecondChallenge store infra)
            routef "/challenge3/%s" (Commands.submitAnswerToThirdChallenge store infra)
        ]
        route "{*url}" notFoundHandler
    ]

module Database =
    open Microsoft.Data.Sqlite
    open System.Text.Json
    open System.Text.Json.Serialization
    open Dapper

    let private jsonOptions =
        JsonFSharpOptions.Default().ToJsonSerializerOptions()

    let private createConnection () =
        new SqliteConnection "Data Source = feartheponies.db"

    let createTables () =
        use connection = createConnection ()
        connection.Execute "
            CREATE TABLE IF NOT EXISTS events (id INTEGER PRIMARY KEY, aggregate_name TEXT, aggregate_id TEXT, data TEXT);
            CREATE TABLE IF NOT EXISTS readmodels (id INTEGER PRIMARY KEY, readmodel_name TEXT, readmodel_id TEXT, data TEXT, UNIQUE(readmodel_name, readmodel_id));
        "

    let insertTeamEvent (aggregateId: string) evt =
        use connection = createConnection ()
        connection.Execute("INSERT INTO events (aggregate_id, aggregate_name, data) VALUES (@aggregateId, @aggregateName, @data)", dict [
            "aggregateId", box aggregateId
            "aggregateName", box "team"
            "data", box (JsonSerializer.Serialize(evt, jsonOptions))
        ])
        |> ignore

    let getTeamEvents (aggregateId: string) =
        use connection = createConnection ()
        connection.Query<string>("SELECT data FROM events WHERE aggregate_id = @aggregateId AND aggregate_name = @aggregateName", dict [
            "aggregateId", box aggregateId
            "aggregateName", box "team"
        ])
        |> Seq.map (fun data -> JsonSerializer.Deserialize<Domain.Team.Events>(data, jsonOptions))
        |> List.ofSeq

    let upsertReadmodelData (readmodelName: string) (readmodelId: string) data =
        use connection = createConnection ()
        connection.Execute("INSERT INTO readmodels (readmodel_id, readmodel_name, data) VALUES (@readmodelId, @readmodelName, @data) ON CONFLICT(readmodel_id, readmodel_name) DO UPDATE SET data = @data", dict [
            "readmodelId", box readmodelId
            "readmodelName", box readmodelName
            "data", box data
        ])
        |> ignore

    let getReadmodelData (readmodelName: string) (readmodelId: string) =
        use connection = createConnection ()
        connection.QueryFirstOrDefault<string>("SELECT data FROM readmodels WHERE readmodel_id = @readmodelId AND readmodel_name = @readmodelName", dict [
            "readmodelId", box readmodelId
            "readmodelName", box readmodelName
        ])
        |> Option.ofObj

    let getAllReadmodelData (readmodelName: string) =
        use connection = createConnection ()
        connection.Query<string>("SELECT data FROM readmodels WHERE readmodel_name = @readmodelName", dict [
            "readmodelName", box readmodelName
        ])
        |> List.ofSeq

module Program =
    open Microsoft.AspNetCore.Builder
    open Giraffe
    open Giraffe.EndpointRouting

    open Argu

    type CliArguments =
        | Duration of duration:int

        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | Duration _ -> "specify duration in minutes"

    [<EntryPoint>]
    let main args =
        Database.createTables () |> ignore

        let endOfTheWorld =
            let parser = ArgumentParser.Create<CliArguments>()
            let results = parser.Parse args

            results.TryGetResult Duration
            |> Option.defaultWith(fun () ->
                match System.Int32.TryParse(System.Environment.GetEnvironmentVariable("DURATION")) with
                | true, duration -> duration
                | _ -> 90)
            |> System.DateTime.Now.AddMinutes

        let store: Readmodels.Store = {
            Get = Database.getReadmodelData
            GetAll = Database.getAllReadmodelData
            Save = Database.upsertReadmodelData
        }

        let infra: Services.Infra = {
            EndOfTheWorld = endOfTheWorld.ToString("yyyy/MM/dd HH:mm:ss")
            GetNow = fun () -> System.DateTime.Now
            GetEvents = Database.getTeamEvents >> Ok
            StoreEvents = fun id events ->
                events
                |> List.iter (Database.insertTeamEvent id)
                |> fun _ -> Readmodels.handle store id events
                |> Ok
        }

        let builder = WebApplication.CreateBuilder()

        builder.Services.AddGiraffe() |> ignore

        let app = builder.Build()

        app
            .UseStaticFiles()
            .UseRouting()
            .UseGiraffeErrorHandler(Http.errorHandler)
            .UseEndpoints(fun e -> e.MapGiraffeEndpoints(Http.endpoints store infra))
            |> ignore

        app.Run()

        0
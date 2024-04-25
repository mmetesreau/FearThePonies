module FearThePonies.Leaderboard

type TeamStatus = {
    TeamName: string
    FirstChallengeAttempts: int
    FirstChallengeCompleted: bool
    SecondChallengeAttempts: int
    SecondChallengeCompleted: bool
    ThirdChallengeAttempts: int
    ThirdChallengeCompleted: bool
}

let private challengeStatus isCompleted attempts = $"""
<div class="separatorContainer">
    <div class="separator { if isCompleted then "completed" else "" }"></div>
</div>
<div>
    <div class="challenge { if isCompleted then "completed" else "" }">
        <div class="trophy">
            <img src="/img/{ if isCompleted then "unlock.png" else "lock.png" }">
        </div>
        <div class="detail">
            <div>
                {attempts} attempt(s)
            </div>
        </div>
    </div>
</div>
"""

let renderTeamStatus teamsStatus =
    match teamsStatus with
    | [] -> """<div class="teamStatusContainer">Waiting players to join...</div>"""
    | teamsStatus ->
        teamsStatus
        |> List.map (fun teamStatus -> $"""
            <div class="teamStatusContainer">
                <div class="teamStatus">
                    <div class="teamName">{teamStatus.TeamName}</div>
                    { challengeStatus teamStatus.FirstChallengeCompleted teamStatus.FirstChallengeAttempts }
                    { challengeStatus teamStatus.SecondChallengeCompleted teamStatus.SecondChallengeAttempts }
                    { challengeStatus teamStatus.ThirdChallengeCompleted teamStatus.ThirdChallengeAttempts }
                </div>
            </div>""")
        |> fun html -> System.String.Join("", html)
    |> fun html -> $"""<div hx-get="/teams-status" hx-trigger="load delay:10s" hx-swap="outerHTML">{html}</div>"""

let render endOfTheWorld = Layout.renderWithScript "Go! Go! Let's go!!!!!" "" $"""
<div class="messageContainer">
    <div class="message">
        <div class="author">
            <img src="/img/pinkie2.png">
            <i>your spymaster</i>
        </div>

        <div class="content">
            <h1>Leaderboard</h1>
            <p>
                Go! Go! Let's go!!!!!
            </p>
            <p id="countdown" data-end-of-the-world='{endOfTheWorld}'></p>
        </div>
    </div>
</div>
<div hx-get="/teams-status" hx-trigger="load" hx-swap="outerHTML"></div>"""

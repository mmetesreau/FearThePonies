module FearThePonies.Register

let private container content = $"""
<div class="formContainer">
    <h1>Time to register!</h1>
    {content}
</div>"""

let private form error = container $"""
<form method="post">
    <label for="teamName">What is your team name:</label>
    <div>
        <input class="field" id="teamName" type="text" name="teamName"> <span class="error">{ Option.defaultValue "" error }</span>
    </div>
    <button class="btn">Register</button>
</form>"""

let private authToken token = container $"""
<div>
    <label>Below your auth token. Don't lose it, it's needed to submit your anwser :)</label>
    <div>
        <input type="text" value="{token}" disabled>
    </div>
    <a href="/challenge1/{System.Uri.EscapeDataString token}" class="btn">Begin</a>
</div>"""

let renderSuccess token =
    authToken token
    |> Layout.render "Time to register!"

let renderError error =
    form (Some error)
    |> Layout.render "Time to register!"

let render () =
    form None
    |> Layout.render "Time to register!"

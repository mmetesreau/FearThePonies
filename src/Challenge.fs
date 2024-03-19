module FearThePonies.Challenge

type ChallengeStatus = { CompletedAt: System.DateTime option }

let form challenge token = $"""
<form method="post" action="/{challenge}/{System.Uri.EscapeDataString token}">
    <label for="answer">What is your answer:</label>
    <div>
        <input id="answer" type="text" name="answer">
    </div>
    <button class="btn">Submit</button>
</form>"""
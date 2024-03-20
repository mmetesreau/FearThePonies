module FearThePonies.FirstChallenge

let render endOfTheWorld token completedAt = Layout.render "Secret battle plan to take over the world" $"""
<div class="messageContainer">
    <div class="message">
        <div class="author">
            <img src="/img/spymaster2.png">
            <i>your spymaster</i>
        </div>

        <div class="content">
            <h1>You have a new message!</h1>
            <p>
                The website is protected with a 4 digits code pin and no one in other agencies has been able to write a proper program to break it. Start your mission by writing a program to brute force the pin code.
            </p>
            <p id="countdown" data-end-of-the-world='{endOfTheWorld}'></p>
        </div>
    </div>
</div>
<div class="formContainer">
    { if Option.isSome completedAt then $"<div class='welldone'>Good answer, <a href='/challenge2/{System.Uri.EscapeDataString token}'>keep up the good work</a>.</div>" else "" }
    <div>
        { if Option.isNone completedAt then Challenge.form "challenge1" token else "" }
    </div>
</div>"""

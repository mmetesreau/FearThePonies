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
                Alert! It seems that our utmost fear has become reality. We have intercepted information indicating that they would have partnered with the allies. We need to gather intel about this new strength in order to foil their plans. The message intercepted might tell us the identity of their new allies. You have to decipher it right now!
            </p>
            <p id="countdown" data-end-of-the-world='{endOfTheWorld}'></p>
        </div>
    </div>
</div>
<div class="formContainer">
    { if Option.isSome completedAt then $"<div class='welldone'>Good answer, <a href='/challenge2/{System.Uri.EscapeDataString token}'>keep up the good work</a>.</div>" else "" }
    <div>
        We don’t have many clues of what to do to decipher it but here they are:<br>
        - We know the key they used is 42<br>
        - They apply a simple binary operation on each byte to revert some bit<br>
        Here are the base64 encoded data :<br>
        bENES0ZGUwoLCn1PCkJLXE8KSUZFWU9OCl5CTwpOT0tGCl1DXkIKXkJPClhLSEhDXlkKCwp+Qk9TCl1DRkYKXUVYQQpdQ15CCl9ZCktETgpdTwpdQ0ZGCk9ETgpCX0dLRENeUwpLRE4KXktBTwpFXE9YCl5CTwpdRVhGTgoLCmdLQU8KWV9YTwpPXE9YU15CQ0RNCkNZClhPS05TCkxFWApeQk8KWkZLRAQKZV9YCkRPUl4KR09ZWUtNTwpdQ0ZGCl5PRkYKU0VfCl1CT0QKXU8KRktfRElCCl5CTwpLXl5LSUEECmhPClhPS05TBA==<br>
    </div>
    <div>
        { if Option.isNone completedAt then Challenge.form "challenge1" token else "" }
    </div>
</div>"""

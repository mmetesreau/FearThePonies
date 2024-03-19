module FearThePonies.SecondChallenge

let render endOfTheWorld token completedAt = Layout.render "Secret battle plan to take over the world" $"""
<div class="messageContainer">
    <div class="message">
        <div class="author">
            <img src="/img/spymaster3.png">
            <i>your spymaster</i>
        </div>

        <div class="content">
            <h1>You have a new message!</h1>
            <p>
                Congratulations agent! You just accessed the communications of SecFail. The only one clue we have to decrypt this message is [A ---> F]. Keep up the good work. <b>Alea jacta est!</b>
            </p>
            <p id="countdown" data-end-of-the-world='{endOfTheWorld}'></p>
        </div>
    </div>
</div>
<div class="formContainer">
    { if Option.isSome completedAt then $"<div class='welldone'>Well done, <a href='/challenge3/{System.Uri.EscapeDataString token}'>let's continue like that</a>.</div>" else "" }
    <div>
        ny xjjrx ymfy tzw nshwjingqj jshwduynts fqltwnymr<br>
        mfx gjjs htruwtrnxji. kwtr stb, bj bnqq zxj fs jshwduynts pjd.<br>
        yt ijhdumjw tzw htrrzsnhfyntsx<br>
        1 - hqjfs ymj pjd gd wjrtansl jajwd wjizsifsy qjyyjw.<br>
        2 - rfu ymj pjd yt ymj knwxy qjyyjw tk ymj fqumfgjy.<br>
        3 - htruqjyj ymj yfgqj bnym ymj wjxy tk ymj fqumfgjy xyfwynsl kwtr ymj qfxy qjyyjw tk ymj pjd.<br><br>
        <u>jcfruqj bnym rdxjhwjy fx ymj pjd</u><br><br>
        fghijklmnopqrstuvwxyzabcde<br>
        rdxjhwyzabcefgiklmnopqstuv<br>
        hzwwjsyqd, bj fwj zxnsl kjfwutsnjx fx pjd<br>
        zxj ny yt ijhwduy edcxuikhuidfmlu. ymnx nx ymj ufxxbtwi yt fhhjxx tzw gfyyqj uqfs bnym ymj ktqqtbnsl ktwr.<br><br>
    </div>
    <div>
        { if Option.isNone completedAt then Challenge.form "challenge2" token else "" }
    </div>
</div>"""
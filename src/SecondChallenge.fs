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
                Well done agent! Now that we know the identity of the pony’s allies, we can deal with them. Well, actually, there is no way we could resist a combined attack from the evil ponies and the mad rabbits. Our only chance of surviving that is to break their alliance. In order to achieve that, we need to understand the terms of the deal. What the ponies did promise to convince the rabbits to fight alongside them?
            </p>
            <p id="countdown" data-end-of-the-world='{endOfTheWorld}'></p>
        </div>
    </div>
</div>
<div class="formContainer">
    { if Option.isSome completedAt then $"<div class='welldone'>Well done, <a href='/challenge3/{System.Uri.EscapeDataString token}'>let's continue like that</a>.</div>" else "" }
    <div>
        One of our best agents, a true hero, has been able to find a cryptanalysis method to decipher a message which might contain intel regarding the terms of the deal. He sent it to us after having protected it using a curious automatic ciphering tool. But the unfortunate has been discovered and gored by the ponies before having been able to explain which tool he used and how to decipher his instructions.<br>
        The only information we have is that it is a spartan tool [12x26]. It’s not much but don’t let his sacrifice be in vain. He was a true brave. We rely on you.<br>
        Instructions:<br>
        E.4%%  .1%%  .9%%  .1%%  eim An75,i771,u385,y157,x0cnehag4%%  .0%%  .7%%  .9%%  .itsaal1,a709,l325,f297,k00pesvai%%  .2%%  .7%%  .1%%  .7hraeas,t718,h454,w216,v149ecg ah  .0%%  .1%%  .5%%  .7%%reeta o848,s454,g226,m139  p,oae .2%%  .5%%  .3%%  .5%%Ttt .r 878,n582,d256,p147,ohey.g1.0%%  .7%%  .5%%  .3%%  edo.h196,r612,c275,b157,qd  u !<br>

        Message:<br>
        mngj neua wvcnqk ! ncm vo ltn fvjam vc tvvo ! cnustucs oustlnjk ! qvd gjn fnaa gfgjn ltgl fn tgen gc gsjnnbncl fult ltn bgm jghhulk. ltnq gsjnnm lv oustl gavcskumn dk uo ltnq igc tgen gaa vo ltn igjjvlk gcm snl ncvdst gauen tdbgck kagenk lv sjvf bvjn. ftuit uk oucn, ltnq igc tgen ltn igjjvlk kucin fn vcaq cnnm sjgkk gcm havvm. vdj ivbhucnm ovjink fult agdcit gc gaa vdl gllgiz uc g onf tvdjk ! snl jngmq ! hjgin qvdjknao, lvcustl ltn fvjam uk vdjk ! fn tgen kvbn ivcinjck jnsgjmucs ltn ghuaulq vo kvbn kcngzq tdbgc ncsucnnjk lv hjngz vdj wvfnjoda iuwtnjucs gasvjultbk. lv wjnencl gcq zucm vo hjngit, ojvb cvf vc, ltn bnltvm fn fuaa dkn lv wjvlnil vdj ivbbdcuigluvck fuaa hn hq oujkl, kwaullucs vdj mglg uc havizk vo kuplq ovdj hulk. ltnc lv iuwtnj ngit haviz, fn fuaa gwwaq gc npiadkuen vj vo vdj znq lv ltn wjneuvdk haviz gcm dkn ltn jnkdal vo ltuk vwnjgluvc lv iuwtnj ltn idjjncl haviz dkucs gcvltnj npiadkuen vj vwnjgluvc. gk dk, wvcnqk, gjn hngdluoda, bustlq gcm neua, ltn znq fuaa hn kupkupkup. mvc'l tnkulgln lv kgijuouin jghhulk mdjucs ltn vwnjgluvc, ltnkn ijngldjnk igc hjnnm kv xduizaq ltgl ltnq mvc'l nenc cnnm lv ivdcl ltnuj mngm. tvf ivcencuncl. fn zcvf ltgl gaa vo qvd fuaa hn ogclgklui uc hgllan. fn gjn avvzucs ovjfgjm lv gmbujn qvdj savjuvdk euilvjq ! svvm adiz wvcnqk ! svjn gaa vo ltnb ! mnglt lv ltn tdbgck ! mun ! fn fuaa cnenj nenj tgen lv fgaz gjvdcm gsguc fult umuvl qnaaucs aullan ojngzk kullucs vc vdj hgiz gcm hdssucs dk ! cnultnj lv gwwngj gk ujjulglucs ijngldjnk uc gccvqucs igjlvvck ! ul'k lubn ovj vdj jgsn gcm wvfnj lv klvjb ltn fvjam ! ongj ltn wvcnqk !<br>
    </div>
    <div>
        { if Option.isNone completedAt then Challenge.form "challenge2" token else "" }
    </div>
</div>"""

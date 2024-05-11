module FearThePonies.ThirdChallenge

let render endOfTheWorld token completedAt = Layout.render "Secret battle plan to take over the world" $"""
<div class="messageContainer">
    <div class="message">
        <div class="author">
            <img src="/img/spymaster4.png">
            <i>your spymaster</i>
        </div>

        <div class="content">
            <h1>You have a new message!</h1>
            <p>
                Great job agent! You have never been that close to defeat SecFail. Though, they deployed a new powerful encryption algorithm. Fortunately, we have been able to obtain the specifications of their algorithm. If you can implement it, we should be able to obtain their final plan. The specifications are available <a href="https://en.wikipedia.org/wiki/Vigenère_cipher" target="_blank">here</a> and they use <b>canyoudoit</b> as key.
            </p>
            <p id="countdown" data-end-of-the-world='{endOfTheWorld}'></p>
        </div>
    </div>
</div>
<div class="formContainer">
    { if Option.isSome completedAt then $"<div class='welldone'>Kudos, <a href='/leaderboard'>you made it</a>.</div>" else "" }
    <div>
        aoh yfy wcw lvrblu! qh viog tb ewph ix yqr amk. qh kqen grr wh wccvj wvrv sri eagn jc kcoz ptxe smihg o jxvtrp dfdb igf a pmijos axpibp giihette rlucqsmku.<br>
        wugzy zoqmknt, wco fov kgaq rvcv dwxo bl esldfl wg nrpjuo pmvcufc, kyoz, qm ks ecofom ipgsbks.<br>
        yo rmlfipfoxr<br>
        xm lwif js néqépzxwx, - yc jyxt, - t'bpcblgioé,<br>
        zm itiaas x'decbvavls à fd hwnt aomzch :<br>
        ai lguyc snrwtx gsg kclws, - mm ooa jink qwgutrjzé<br>
        jrfbx ne fmzylz vhkr qc zu pézigeoygs.<br>
        xdba ec nhgh xx hwfdens, hil ecb o'af achvcté,<br>
        kgnqq-ail zm icufgzcsdm xv ln ksl g'wbtnir,<br>
        jo zosck suv nzulgibv tnlh à grb khgue bégioé,<br>
        sb ec tecwfos wù eg pnkdlh à zi kqsr q'ofowm.<br>
        lwif-hs upcck qu cfépov ?... zclkgayb ix pqkqn ?<br>
        zmb zucvm gsg pcojs mgeoe bi vdwaxt dr jo lhwvx ;<br>
        l'av pêjé xdba ec gemhnh cù vtie yy gcuèbm...<br>
        xv j'ng ryxl nhks iywhtimnt teyjyugé t'tehéemb :<br>
        grrcecng rcou à hwnt shp zu omzx f'oenvéy<br>
        osa lqucgfm gs tt uavlhy hh txu cegg xh zi yég.<br>
        ig'q pydibbhuy ggh'w wb?<br>
        uwt jywn, wc ebp, ybs vuys bh io ucfy /ggifhdf?ybmzsz=[yklyuwnkhpxcnfusl]&dibavoxcb=[ovsghwrgmyyq] ovw rrbtwxh ovlyee rc nks cevizyhy timlvibl cz ownx, vhr sbcyszlg, aab sphfgmjiae!<br>
    </div>
    <div>
        { if Option.isNone completedAt then Challenge.form "challenge3" token else "" }
    </div>
</div>"""

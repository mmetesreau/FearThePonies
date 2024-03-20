module FearThePonies.Welcome

let render () = Layout.render "Secret battle plan to take over the world" """
<div class="messageContainer">
    <div class="message">
        <div class="author">
            <img src="/img/spymaster1.png">
            <i>your spymaster</i>
        </div>

        <div class="content">
            <h1>Breaking news: we are under attack!</h1>
            <p>
                Ponies just forged a secret alliance in order to end human civilization and their first move should happen in less than 1 hour. We require your help to access their information which should allow us to arrange a strike back plan or at least buy us time. Start your mission and don't fail. You're our only hope...
            </p>
            <div class="actions">
                <a href="/register" class="action">Let the battle begin!</a>
            </div>
        </div>
    </div>
</div>"""
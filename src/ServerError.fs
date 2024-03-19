module FearThePonies.ServerError

let render message = Layout.render "Oups" $"""
<div class="messageContainer">
    <div class="message">
        <div class="author">
            <img src="/img/spymaster3.png">
            <i>your spymaster</i>
        </div>

        <div class="content">
            <h1>Oups :/</h1>
            <p>
                {message}
            </p>
            <div class="actions">
                <a href="/" class="action">Back to Home</a>
            </div>
        </div>
    </div>
</div>"""
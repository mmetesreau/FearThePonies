module FearThePonies.NotFound

let render () = Layout.render "Hey sweetie, what are you looking for?" """
<div class="messageContainer">
    <div class="message">
        <div class="author">
            <img src="/img/spymaster3.png">
            <i>your spymaster</i>
        </div>

        <div class="content">
            <h1>Hey sweetie, what are you looking for?</h1>
            <p>
                There is nothing to see here.
            </p>
            <div class="actions">
                <a href="/" class="action">Back to Home</a>
            </div>
        </div>
    </div>
</div>"""


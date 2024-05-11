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
                Great job agent! Now that we know the terms of the deal, it’s possible to break their alliance and defeat the ponies. The ponies should deliver the stock of carrot they currently possess to the rabbits before the battle. You have to help us again to find out about the place and time of the delivery. If you can do that, we will intercept and steal their carrots. Also we already sent orders to our teams everywhere in the world to have them ready to destroy every single existing carrot as soon as we will have intercepted the stock of the ponies. Burning to the ground all of the carrots in the world should make the rabbits pull out since they won’t be able to get what they want.
            </p>
            <p id="countdown" data-end-of-the-world='{endOfTheWorld}'></p>
        </div>
    </div>
</div>
<div class="formContainer">
    { if Option.isSome completedAt then $"<div class='welldone'>Kudos, <a href='/leaderboard'>you made it</a>.</div>" else "" }
    <div>
    </div>
        Find the place of the delivery by deciphering the following message:<br>
        WYIgdW5lIHCi81MUABFFejK9MjRyZCBaycxBWwcWRDMgvSA1c3NkUs/LT0ABUwA3daQgKSE7dUWDx0ldDzE5KneiPDgjEVRDg8NZFAN0OmN+szh6Z1ReBpHYVFZHMDFzZ78hJGddUBmYzlVRAigjfC7GAD9nCEUZ2allHxdpNmoih0V7MBxYD5joJBJePD5ucZ5RdytPW2S48yQbTjk6ClbdBHkvVVtkD3hlF1t1NwG1HABkLxpZIUpqIAgIdSxTvA1UKDN/JhJBZjhNExpSMrULWiF2NnJTWWw5AQVXUjmiA1tkJTM3GUt1OhBQVhkTnBhTPHA8fDNkbyVdGU9QE50fTC5pjPkzZHIhQwysjF3eFVk3fs36PCN2N0NSx75d1wcXMD2pnjIobHscHcr3V95OF3Vro5MyZCPUzEvE9kCTRPSgbKuDMmghlc5Aoc9T0kf6uyPEuiFoNI/SA6LbUpFf4bcjx69yZzjBx0+mxgGUSOG2Os/mdXsvz7wwmohVIoSs0FHz+nuWqIygJJqJW2DLrM5R8/172+mB7heGmhI1gveLN+T/c9r0NCI9oJAdNNZYRx3S9XrPpjxncPWUWjPFVRNQhvsvzaY8fTXrnkEjhE4YW4pd7830KzRRxDjPI5MLQjS2Sq7QvGEnFMYm2zmeEFJx5kK6ze8wPlYl684yn15XIuZC7peXVBZLii6LeOcnOmvoR+6MxUtVAoZnijGuKDwip0f+2cNYHFbGNZpj4Hh2N6tU84rCCBNC33kwurR6dmL+c3NBxFocRZcUHbS0PzwqVK09WsMfWl893hHgtWp6MVj+YhveGVpem0dC67k5LD/yNG57eK0MS51dTpAPyCwhujg7eX6tDEDTVfhKGYEsg2d1jL9yoV32DlXgQFDSPIBvPJP6c9g2wwdd4QwUqxaBYjyU8nPHd+gQWZ5iNaIWmjA48pRex2W2EHWTany1BNRyHOeDUpVlp1JslXY9/BbCNkC1m1rcYataLJVmMaoEiyND4Nxc33arUDePJTX/Gc1wVO7NRZBt7RIxiDg19U2ZelSowFaBOfUfeqIOPOQZkXoWy+JblmCxDX+nFHn+AcF9GsmuGoohtRV/6UZ9+VXUYA2IsgvZGbFAT+1JfL183SEmn7Ze1BL9cUftRS/6MqkZIoP/Wp8S3nBO70Ur632sHW6bt0zLCsNvAv8NOqRtphtqmuUYhWfycw+6GnThR50VL9L1G4Ap9GFW8gZqoEGVESaX8gHOJrVlSfkBZKZSlURpu/5GxSDgIQWXRCagAI1Ed/S3SMVz/mhXnEw8oFOYHTm89z6gU5gdObw=<br>
    <div>
        { if Option.isNone completedAt then Challenge.form "challenge3" token else "" }
    </div>
</div>"""

window.addEventListener("load", _ => {
    const createUpdateCountdown = end => () => {
        const now = new Date().getTime();
        const distance = end - now;

        const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));

        let message = "Hurry up, the ponies will strike in ";

        if (hours > 0) message += hours + " hour(s)"

        if (minutes > 0) {
            if (hours > 0) message += " and "
            message += minutes + " minute(s)"
        }

        message += "."

        countdownElt.innerHTML = message
    }

    const countdownElt = document.querySelector("#countdown");

    if (!countdownElt) return;

    const end = new Date(countdownElt.dataset.endOfTheWorld).getTime();
    const updateCountdown = createUpdateCountdown(end);

    updateCountdown()

    const id = setInterval(updateCountdown, 60000);

    window.addEventListener("beforeunload", _ => { clearInterval(id); })
})




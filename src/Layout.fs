module FearThePonies.Layout

let renderWithScript title script content = $"""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>{title}</title>
    <link rel="icon" type="image/png" sizes="16x16" href="/favicon.png" />
    <link href='https://fonts.googleapis.com/css?family=Lato:400,700' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="/css/app.css" />
</head>
<body class="background">
    {content}
    <script type="text/javascript" src="/js/htmx.min.js"></script>
    <script type="text/javascript" src="/js/app.js"></script>
    <script type="text/javascript">
        {script}
    </script>
</body>
</html>"""

let render title content =
    renderWithScript title "" content
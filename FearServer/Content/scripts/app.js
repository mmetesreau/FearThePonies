$(function () {

    $.get('/endoftheworld', function (endoftheworld) {
        $("#countdown").countdown(endoftheworld, function (event) {
            var totalMinutes = event.lasting.minutes + event.lasting.hours * 60;

            if (totalMinutes === 0 && event.lasting.seconds === 0) {
                if (!$("#ponies").length) {
                    $('body').append('<div style="background-color:white;position:absolute;top:0;left:0;bottom:0;right:0;z-index:100;pointer-events: none; overflow: hidden;"><div class="page-container"><div><div class="row vertical-align"><div class="col-xs-2"><div class="circular-logo pull-right"></div></div><div class="col-xs-10"><h1>You have got a message from Spymaster</h1><p class="text-xs-justify">Hahahaha we got you, ponies won this time!!!!</p></div></div></div><div id="ponies" style="position: relative;overflow:hidden;height:60px;"></div></div></div>');

                    PonyStream.load([
                       '/Content/img/ponies/af.gif',
                       '/Content/img/ponies/bm.gif',
                       '/Content/img/ponies/cc.gif',
                       '/Content/img/ponies/cs.gif',
                       '/Content/img/ponies/dl.gif',
                       '/Content/img/ponies/ib.gif',
                       '/Content/img/ponies/jl.gif',
                       '/Content/img/ponies/km.gif',
                       '/Content/img/ponies/kr.gif',
                       '/Content/img/ponies/lw.gif',
                       '/Content/img/ponies/mk.gif',
                       '/Content/img/ponies/mt.gif',
                       '/Content/img/ponies/mu.gif',
                       '/Content/img/ponies/ni.gif',
                       '/Content/img/ponies/nk.gif',
                       '/Content/img/ponies/rs.gif',
                       '/Content/img/ponies/tj.gif',
                       '/Content/img/ponies/tl.gif',
                       '/Content/img/ponies/zf.gif'
                    ]);

                    PonyStream.start(ponies);
                }
            } else {
                $(this).html('Hurry up, only ' + totalMinutes + ' minutes and ' + event.lasting.seconds + ' secondes left');
            }
        });
    });

    var notificationHub = $.connection.notificationHub;

    $.connection.hub.start().done(function () {});

    notificationHub.client.send = function (achievement) {
        showAchievement(achievement);
    }

    function showAchievement(achievement) {
        toastr.success(achievement.pseudo + " hacked step " + achievement.step);
    }
});
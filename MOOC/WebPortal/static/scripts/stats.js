function start_counter(number) {
    counter = counter + parseInt(number);
    if (clear_counter != 'true') {
        var t = setTimeout("start_counter('1');", 1000);
    }
}

function getStats() {
    var totalSec = parseInt(counter);
    var minutes = parseInt(totalSec / 60);
    var raw_minutes = parseFloat(totalSec / 60);
    var seconds = totalSec % 60;
    var accuracy = parseInt(((level_keys.length - keysmissed_number) / level_keys.length) * 100);
    var wpm_errors = keysmissed_number / raw_minutes;
    var wpm = (level_keys.length / 5) / raw_minutes;
    var nwpm = wpm - wpm_errors;

    if (parseInt(wpm) > 0) { } else { wpm = 0; }
    if (parseInt(nwpm) > 0) { } else { nwpm = 0; }


    $("#alert_frame").html("");

    var container = $("<div>");
    $(container).attr("class", "ScoreContainer");
    $(container).attr("border", "none !important");
    $(container).attr("margin-top", "0px !important");

    var timeCont = $("<div>");
    $(timeCont).attr("class", "Score");
    $(timeCont).html("<span class='scoreSpan'>Time Spent:</span><b>" + minutes + "m:" + seconds + "s</b>")
    $(container).append(timeCont);

    if (keysmissed != "") {
        var keMissed = $("<div>");
        $(keMissed).attr("class", "Score");
        $(keMissed).html("<span class='scoreSpan'>Keys missed:</span><b>" + keysmissed + "</b>");
        //$(container).append(keMissed);
    }

    var acury = $("<div>");
    $(acury).attr("class", "Score");
    $(acury).html("<span class='scoreSpan'>Accuracy:</span><b>" + accuracy + "%</b>");
    $(container).append(acury);

    var grosWPM = $("<div>");
    $(grosWPM).attr("class", "Score");
    $(grosWPM).html("<span class='scoreSpan'>Gross WPM:</span><b>" + parseInt(wpm) + "</b>");
    $(container).append(grosWPM);

    var netWPM = $("<div>");
    $(netWPM).attr("class", "Score");
    $(netWPM).html("<span class='scoreSpan'>Net WPM:</span><b>" + parseInt(nwpm) + "</b>");
    $(container).append(netWPM);

    $("#alert_frame").append(container);

    //document.getElementById('alert_frame').innerHTML = '<div><p><b>Your Score</b></p>';
    //document.getElementById('alert_frame').innerHTML += '<div class="Score">Time Spent:    ' + minutes + 'm:' + seconds + 's</div>';
    //if (keysmissed != "") { document.getElementById('alert_frame').innerHTML += '<div class="Score">Keys missed:   ' + keysmissed + '</div>'; }
    //document.getElementById('alert_frame').innerHTML += '<div class="Score">Accuracy:      ' + accuracy + '%</div>';
    //document.getElementById('alert_frame').innerHTML += '<div class="Score">Gross WPM:     ' + parseInt(wpm) + '</div>';
    //document.getElementById('alert_frame').innerHTML += '<div class="Score">Net WPM:       ' + parseInt(nwpm) + '</div>';
    //document.getElementById('alert_frame').innerHTML += '</div>';

    var timeSpan = minutes + 'm:' + seconds + 's';

    SaveTypingStats(current_level, timeSpan, accuracy, parseInt(wpm), parseInt(nwpm));

    if (level_text.length - 1 == current_level) {
        // congratulations You have completed all 23 levels of typing tutorials. We encourage you to improve your typing speed above (last number) along with accuracy above(last number) by visiting level 1 again.
        $(".content-container").html("<div style='margin:10%;font-size: 20px;line-height: 30px;'><span style='font-size: 24px; font-weight: bold;'>Congratulations</span> You have completed all " + (level_text.length - 1) + " levels of typing tutorials. We encourage you to improve your typing speed above(" + parseInt(wpm) + " WPM) along with accuracy above(" + accuracy + "%) by visiting <a href='TypingTutorial.aspx' target='_top'>level 1</a> again.</div>");
    }
}
function change_key_color(key) {
    document.getElementById(key.toLowerCase()).style.backgroundColor = '#51a351';
    document.getElementById(key.toLowerCase()).style.color = '#FFF';

    var string = 'QWERTYASDFGZXCVB';
    string = string.split('');
    for (var i = 0; i < string.length; i++) {
        if (key == string[i]) {
            document.getElementById('LShift').style.backgroundColor = '#51a351';
            document.getElementById(key.toLowerCase()).style.color = '#FFF';
        }
    }

    var string = 'UIOPHJKLNM';
    string = string.split('');
    for (var i = 0; i < string.length; i++) {
        if (key == string[i]) {
            document.getElementById('RShift').style.backgroundColor = '#51a351';
            document.getElementById(key.toLowerCase()).style.color = '#FFF';
        }
    }
}

function revert_key_color(key) {
    if (key != undefined) {
        document.getElementById(key.toLowerCase()).style.backgroundColor = 'white';
        document.getElementById(key.toLowerCase()).style.color = "#999";
    }
}

function buildKeyboard() {
    var keys = "~,1,2,3,4,5,6,7,8,9,0,-,=";
    var keys = keys.split(",");

    for (i = 0; i < keys.length; i++) {
        document.getElementById('keypad').innerHTML += '<div id="' + keys[i] + '" class="key">' + keys[i] + '</div>';
    }

    document.getElementById('keypad').innerHTML += '<div id="Bkspc" class="splkey">Bkspc</div><div style="clear:both"></div>';

    document.getElementById('keypad').innerHTML += '<div id="Tab" class="splkey2">Tab</div>';

    var keys = "Q,W,E,R,T,Y,U,I,O,P,[,]";
    var keys = keys.split(",");

    for (i = 0; i < keys.length; i++) {
        document.getElementById('keypad').innerHTML += '<div id="' + keys[i].toLowerCase() + '" class="key">' + keys[i] + '</div>';
    }

    document.getElementById('keypad').innerHTML += '<div id="\\" class="splkey2" style="background-color: white">\\</div><div style="clear:both"></div>';

    document.getElementById('keypad').innerHTML += '<div id="Caps" class="splkey4">Caps</div>';

    var keys = "A,S,D,F,G,H,J,K,L,;,'";
    var keys = keys.split(",");

    for (i = 0; i < keys.length; i++) {
        document.getElementById('keypad').innerHTML += '<div id="' + keys[i].toLowerCase() + '" class="key">' + keys[i] + '</div>';
    }

    document.getElementById('keypad').innerHTML += '<div id="Enter" class="splkey4">Enter</div><div style="clear:both"></div>';

    document.getElementById('keypad').innerHTML += '<div id="LShift" class="splkey5">Shift</div>';

    var keys = "Z;X;C;V;B;N;M;,;.;/";
    var keys = keys.split(";");

    for (i = 0; i < keys.length; i++) {
        document.getElementById('keypad').innerHTML += '<div id="' + keys[i].toLowerCase() + '" class="key">' + keys[i] + '</div>';
    }

    document.getElementById('keypad').innerHTML += '<div id="RShift" class="splkey5">Shift</div><div style="clear:both"></div>';

    document.getElementById('keypad').innerHTML += '<div id="LCtrl" class="splkey2">Ctrl</div>';

    document.getElementById('keypad').innerHTML += '<div id="LAlt" class="splkey2">Alt</div>';

    document.getElementById('keypad').innerHTML += '<div id=" " class="splkey2" style="width: 285px; background-color: white"></div>';

    document.getElementById('keypad').innerHTML += '<div id="RAlt" class="splkey2">Alt</div>';

    document.getElementById('keypad').innerHTML += '<div id="RCtrl" class="splkey2">Ctrl</div><div style="clear:both"></div>';
}
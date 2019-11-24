function buildKeysTable() {
    for (i = 0; i < level_keys.length; i++) {
        if (i < 386) {
            document.getElementById('keys_to_type').innerHTML += '<div id="typekey' + i + '" style="width:14px; height:25px; line-height:25px; float:left" align="center">' + level_keys[i] + '</div>';
            if ((i + 1) % 32 == 0) {
                //document.getElementById('keys_to_type').innerHTML += '<div style="clear:both"></div>';
            }
        }
    }
}
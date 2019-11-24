var LOCATIONS = null;

var GetStoreLocationControl = function (containerClass, storeLocations) {
    var room = [];
    var cupboard = [];
    var shelf = [];
    var files = [];

    if (LOCATIONS == null)
        LOCATIONS = storeLocations;

    // Populate room
    for (i = 0; i < LOCATIONS.length; i++) {
        if (room.indexOf(LOCATIONS[i].RoomNumber) == -1) {
            room.push(LOCATIONS[i].RoomNumber);
        }
    }

    var ddRoom = GetDropdown("Room", room);
    var ddCupboard = GetDropdown("Cupboard", cupboard);
    var ddShelfs = GetDropdown("Shelf", shelf);
    var ddFiles = GetDropdown("File", files);

    $(ddRoom).on("change", function () {
        var selectedRoom = $(this).find("option:selected").text();

        $(ddCupboard).children().remove();
        cupboard.length = 0;
        for (i = 0; i < LOCATIONS.length; i++) {
            if (LOCATIONS[i].RoomNumber == selectedRoom) {
                if (cupboard.indexOf(LOCATIONS[i].Cupboard) == -1) {
                    cupboard.push(LOCATIONS[i].Cupboard);
                    $(ddCupboard).append($("<option/>").attr("value", LOCATIONS[i].Cupboard).html(LOCATIONS[i].Cupboard));
                }
            }
        }

        $(ddCupboard).append($("<option/>").attr("value", "-1").html("---Cupboard---").attr("selected", "true"));
    });

    $(ddCupboard).on("change", function () {
        var selectedCupboard = $(this).find("option:selected").text();

        $(ddShelfs).children().remove();
        shelf.length = 0;

        for (i = 0; i < LOCATIONS.length; i++) {
            if (LOCATIONS[i].RoomNumber == $(ddRoom).find("option:selected").text()) {
                if (LOCATIONS[i].Cupboard == selectedCupboard) {
                    if (shelf.indexOf(LOCATIONS[i].Shelf) == -1) {
                        shelf.push(LOCATIONS[i].Shelf);
                        $(ddShelfs).append($("<option/>").attr("value", LOCATIONS[i].Shelf).html(LOCATIONS[i].Shelf));
                    }
                }
            }
        }

        $(ddShelfs).append($("<option/>").attr("value", "-1").html("---Shelf---").attr("selected", "true"));
    });

    $(ddShelfs).on("change", function () {
        var selectedShelf = $(this).find("option:selected").text();

        $(ddFiles).children().remove();
        files.length = 0;

        for (i = 0; i < LOCATIONS.length; i++) {
            if (LOCATIONS[i].RoomNumber == $(ddRoom).find("option:selected").text()) {
                if (LOCATIONS[i].Cupboard == $(ddCupboard).find("option:selected").text()) {
                    if (LOCATIONS[i].Shelf == selectedShelf) {
                        if (files.indexOf(LOCATIONS[i].FileName) == -1) {
                            files.push(LOCATIONS[i].FileName);
                            $(ddFiles).append($("<option/>").attr("value", LOCATIONS[i].FileName).html(LOCATIONS[i].FileName));
                        }
                    }
                }
            }
        }

        $(ddFiles).append($("<option/>").attr("value", "-1").html("---File---").attr("selected", "true"));
    });


    $(containerClass).append(ddRoom);

    // Populate the cupboard
    $(containerClass).append(ddCupboard);

    // Populate the shelf
    $(containerClass).append(ddShelfs);

    // Populate the file
    $(containerClass).append(ddFiles);

    // Prepare Room dropdown, cupboard location, shelf, file dropdown
    // Attach the dropdown change event
    // Prepare method which will return the final store location to store it in db
    // prepare method which will parse the DB store location string and populate the dropdown with correct value selected
}

var GetDropdown = function (id, options) {
    var dd = $("<select/>").attr("id", "dd-sl-" + id).attr("class", "dropdown");

    for (var i = 0; i < options.length; i++) {
        $(dd).append($("<option/>").attr("value", options[i]).html(options[i]));
    }

    $(dd).append($("<option/>").attr("value", "-1").html("---" + id + "---").attr("selected", "true"));
    return dd;
}

var SetStoreLocation = function (containerClass, storeLocations) {

    if (storeLocations == null || storeLocations == undefined) {
        storeLocations = LOCATIONS;
    }

    if (storeLocations.length == 1) {
        for (i = 0; i < storeLocations.length; i++) {

            if (storeLocations[i] != "") {
                var loc = storeLocations[i].split(' -&gt; ');

                /* Code commented by vasim becuase giving error on 18-Aug-2014*/

                loc[0] = loc[0].substring(loc[0].indexOf('(') + 1);
                loc[1] = loc[1].substring(loc[1].indexOf('(') + 1);
                loc[2] = loc[2].substring(loc[2].indexOf('(') + 1);
                loc[3] = loc[3].substring(loc[3].indexOf('(') + 1);

                loc[0] = loc[0].substring(0, loc[0].lastIndexOf(")"));
                loc[1] = loc[1].substring(0, loc[1].lastIndexOf(")"));
                loc[2] = loc[2].substring(0, loc[2].lastIndexOf(")"));
                loc[3] = loc[3].substring(0, loc[3].lastIndexOf(")"));

                $("#dd-sl-Room").find("option").each(function () {
                    if ($(this).text() == loc[0]) {
                        $(this).attr("selected", "true");
                    }
                    else {
                        $(this).removeAttr("selected");
                    }
                });

                $("#dd-sl-Room").change();

                $("#dd-sl-Cupboard").find("option").each(function () {
                    if ($(this).text() == loc[1]) {
                        $(this).attr("selected", "true");
                    }
                    else {
                        $(this).removeAttr("selected");
                    }
                });

                $("#dd-sl-Cupboard").change();

                $("#dd-sl-Shelf").find("option").each(function () {
                    if ($(this).text() == loc[2]) {
                        $(this).attr("selected", "true");
                    }
                    else {
                        $(this).removeAttr("selected");
                    }
                });

                $("#dd-sl-Shelf").change();

                $("#dd-sl-File").find("option").each(function () {
                    if ($(this).text() == loc[3]) {
                        $(this).attr("selected", "true");
                    }
                    else {
                        $(this).removeAttr("selected");
                    }
                });
            }
        }
    }
    else {

        for (i = 0; i < storeLocations.length; i++) {

            if (storeLocations[i] != "") {
                var loc = storeLocations[i].split(' -&gt; ');

                loc[0] = storeLocations[0].split(' -&gt; ');
                loc[1] = storeLocations[1].split(' -&gt; ');
                loc[2] = storeLocations[2].split(' -&gt; ');
                loc[3] = storeLocations[3].split(' -&gt; ');

                /* Code commented by vasim becuase giving error on 18-Aug-2014
                loc[0] = loc[0].substring(loc[0].indexOf('(') + 1);
                loc[1] = loc[1].substring(loc[1].indexOf('(') + 1);
                loc[2] = loc[2].substring(loc[2].indexOf('(') + 1);
                loc[3] = loc[3].substring(loc[3].indexOf('(') + 1);
    
                loc[0] = loc[0].substring(0, loc[0].lastIndexOf(")"));
                loc[1] = loc[1].substring(0, loc[1].lastIndexOf(")"));
                loc[2] = loc[2].substring(0, loc[2].lastIndexOf(")"));*/

                $("#dd-sl-Room").find("option").each(function () {
                    if ($(this).text() == loc[0]) {
                        $(this).prop("selected", true);
                    }
                    else {
                        $(this).removeAttr("selected");
                    }
                });

                $("#dd-sl-Room").change();

                $("#dd-sl-Cupboard").find("option").each(function () {
                    if ($(this).text() == loc[1]) {
                        $(this).prop("selected", true);
                    }
                    else {
                        $(this).removeAttr("selected");
                    }
                });

                $("#dd-sl-Cupboard").change();

                $("#dd-sl-Shelf").find("option").each(function () {
                    if ($(this).text() == loc[2]) {
                        $(this).prop("selected", true);
                    }
                    else {
                        $(this).removeAttr("selected");
                    }
                });

                $("#dd-sl-Shelf").change();

                $("#dd-sl-File").find("option").each(function () {
                    if ($(this).text() == loc[3]) {
                        $(this).prop("selected", true);
                    }
                    else {
                        $(this).removeAttr("selected");
                    }
                });
            }
        }
    }
}

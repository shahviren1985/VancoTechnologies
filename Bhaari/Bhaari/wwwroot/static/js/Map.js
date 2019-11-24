/**
* A distance widget that will display a circle that can be resized and will
* provide the radius in km.
*
* @param {google.maps.Map} map The map to attach to.
*
* @constructor
*/
function DistanceWidget(map) {
    this.set('map', map);
    this.set('position', map.getCenter());

    var marker = new google.maps.Marker({
        draggable: true,
        raiseOnDrag: false,
        icon: 'static/img/drags.png'//'static/img/drag.png'//drags.png
    });

    // Bind the marker map property to the DistanceWidget map property
    marker.bindTo('map', this);

    // Bind the marker position property to the DistanceWidget position
    // property
    marker.bindTo('position', this);

    // Create a new radius widget
    var radiusWidget = new RadiusWidget();

    // Bind the radiusWidget map to the DistanceWidget map
    radiusWidget.bindTo('map', this);

    // Bind the radiusWidget center to the DistanceWidget position
    radiusWidget.bindTo('center', this, 'position');

    // Bind to the radiusWidgets' distance property
    this.bindTo('distance', radiusWidget);

    // Bind to the radiusWidgets' bounds property
    this.bindTo('bounds', radiusWidget);

    google.maps.event.addListener(marker, 'dragend', function () {
        chkPointsWithinCircle(distanceWidget)
    });

}
// VS - Uncomment
DistanceWidget.prototype = new google.maps.MVCObject();


/**
* A radius widget that add a circle to a map and centers on a marker.
*
* @constructor
*/
function RadiusWidget() {
    var circle = new google.maps.Circle({
        strokeWeight: 2, strokeColor: '#2B60DE',
        strokeOpacity: 0.8,

        fillColor: '#2B60DE',
        fillOpacity: 0.10
    });

    // Set the distance property value, default to 50km.
    this.set('distance', 1.7);

    // Bind the RadiusWidget bounds property to the circle bounds property.
    this.bindTo('bounds', circle);

    // Bind the circle center to the RadiusWidget center property
    circle.bindTo('center', this);

    // Bind the circle map to the RadiusWidget map
    circle.bindTo('map', this);

    // Bind the circle radius property to the RadiusWidget radius property
    circle.bindTo('radius', this);

    // Add the sizer marker
    this.addSizer_();

}

// VS - Uncomment
RadiusWidget.prototype = new google.maps.MVCObject();


/**
* Update the radius when the distance has changed.
*/
RadiusWidget.prototype.distance_changed = function () {
    this.set('radius', this.get('distance') * 1000);
    this.center_changed();
};


/**
* Add the sizer marker to the map.
*
* @private
*/
RadiusWidget.prototype.addSizer_ = function () {
    var sizer = new google.maps.Marker({
        draggable: true, raiseOnDrag: false,
        icon: 'static/img/resize3.png'
    });

    sizer.bindTo('map', this);
    sizer.bindTo('position', this, 'sizer_position');

    var me = this;
    google.maps.event.addListener(sizer, 'drag', function () {
        // As the sizer is being dragged, its position changes.  Because the
        // RadiusWidget's sizer_position is bound to the sizer's position, it will
        // change as well.
        var min = 0.5;
        var max = 15;
        var pos = me.get('sizer_position');
        var center = me.get('center');
        var distance = google.maps.geometry.spherical.computeDistanceBetween(center, pos) / 1000;
        if (distance < min) {
            me.set('sizer_position', google.maps.geometry.spherical.computeOffset(center, min * 1000, google.maps.geometry.spherical.computeHeading(center, pos)));
        } else if (distance > max) {
            me.set('sizer_position', google.maps.geometry.spherical.computeOffset(center, max * 1000, google.maps.geometry.spherical.computeHeading(center, pos)));
        }
        // Set the circle distance (radius)
        me.setDistance();
    });

    google.maps.event.addListener(sizer, 'dragend', function () {
        chkPointsWithinCircle();
    });
};


/**
* Update the center of the circle and position the sizer back on the line.
*
* Position is bound to the DistanceWidget so this is expected to change when
* the position of the distance widget is changed.
*/
RadiusWidget.prototype.center_changed = function () {
    var bounds = this.get('bounds');

    // Bounds might not always be set so check that it exists first.
    if (bounds) {
        var lng = bounds.getNorthEast().lng();

        // Put the sizer at center, right on the circle.
        var position = new google.maps.LatLng(this.get('center').lat(), lng);
        this.set('sizer_position', position);
    }
};


/**
* Calculates the distance between two latlng points in km.
* @see http://www.movable-type.co.uk/scripts/latlong.html
*
* @param {google.maps.LatLng} p1 The first lat lng point.
* @param {google.maps.LatLng} p2 The second lat lng point.
* @return {number} The distance between the two points in km.
* @private
*/
RadiusWidget.prototype.distanceBetweenPoints_ = function (p1, p2) {
    if (!p1 || !p2) {
        return 0;
    }

    var R = 6371; // Radius of the Earth in km
    var dLat = (p2.lat() - p1.lat()) * Math.PI / 180;
    var dLon = (p2.lng() - p1.lng()) * Math.PI / 180;
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
          Math.cos(p1.lat() * Math.PI / 180) * Math.cos(p2.lat() * Math.PI / 180) *
          Math.sin(dLon / 2) * Math.sin(dLon / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    return d;
};


/**
* Set the distance of the circle based on the position of the sizer.
*/
RadiusWidget.prototype.setDistance = function () {
    // As the sizer is being dragged, its position changes.  Because the
    // RadiusWidget's sizer_position is bound to the sizer's position, it will
    // change as well.
    var pos = this.get('sizer_position');
    var center = this.get('center');
    var distance = this.distanceBetweenPoints_(center, pos);

    // Set the distance property for any objects that are bound to it
    this.set('distance', distance);
};


var map, geocoder, bounds;
var markerList = [];
var dynamicId = 1;
var setDistanceWidget;

//var geocoderRequest = [
//                            { address: 'A Wing, Kensington Sez, Hiranandani, Powai, Mumbai - 400076', DisplayText: '1', DatePosted: '2014/01/23' },
//							{ address: 'sarvodya housing society, Opp durgadevi sharma garden, Gokhale nagar road, IIT Powai , Mumbai - 400076', DisplayText: '2', DatePosted: '2014/03/22' },
//							{ address: 'Lower Depo Pada, Road No 1, Parksite, Vikhroli West, Mumbai - 400079', DisplayText: '3', DatePosted: '2014/03/01' },
//							{ address: 'Adi Sankracharya Marg, Powai, Mumbai - 400076', DisplayText: '4', DatePosted: '2014/03/11' }
//                          ];


function init(lat,long) {
    bounds = new google.maps.LatLngBounds();
    geocoder = new google.maps.Geocoder();
    var mapDiv = document.getElementById('map');
    //var latlng = new google.maps.LatLng(37.09, -95.71);
    var latlng = new google.maps.LatLng(lat, long);

    var options = {
        center: latlng,
        zoom: 14,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        mapTypeControlOptions: { mapTypeIds: [google.maps.MapTypeId.ROADMAP, 'map_style'] },
        disableDefaultUI: true,
        scaleControl: true
    };

    var a = [{ featureType: "all", elementType: "all", stylers: [{ saturation: -80}] }, { featureType: "water", elementType: "all", stylers: [{ lightness: -25}] }, { featureType: "transit.line", stylers: [{ visibility: "off"}] }, { featureType: "administrative", stylers: [{ visibility: "simplified"}] }, { featureType: "poi", stylers: [{ visibility: "off"}]}];

    var styledMap = new google.maps.StyledMapType(a, { name: "Styled Map" });
    map = new google.maps.Map(mapDiv, options);

    map.mapTypes.set('map_style', styledMap);
    map.setMapTypeId('map_style');


    distanceWidget = new DistanceWidget(map);

    google.maps.event.addListener(distanceWidget, 'distance_changed', function () {
        setDistanceWidget = distanceWidget;
    });

    google.maps.event.addListener(distanceWidget, 'position_changed', function () {

        setDistanceWidget = distanceWidget;
    });

    setMarkersOnMap();
}

function parseDate(input) {
    var parts = input.split('/');
    return new Date(parts[0], parts[1] - 1, parts[2]);
}

function GetMarkerColorCode(datePosted) {
    var one_day = 1000 * 60 * 60 * 24;
    var posted = parseDate(datePosted).getTime();
    var currentDate = new Date().getTime();

    var difference_ms = currentDate - posted;

    var diff = Math.round(difference_ms / one_day);

    if (diff >= 30) {
        colorCode = 'bubble-red';
    }
    else if (diff >= 15 && diff < 30) {
        colorCode = 'bubble-yellow';
    }
    else {
        colorCode = 'bubble-green';
    }

    return colorCode;
}

function setMarkersOnMap() {
    var jobjson = JSON.parse($("#hfJobs").val());

    for (var i = 0; i < jobjson.length; i++) {
        GetCoordinates(jobjson[i])
    }
}

var previous_val = '';
function GetCoordinates(details) {
    geocoder.geocode({ address: details.Location }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            // Do something with the response
            map.setCenter(results[0].geometry.location);
            var colorCode = GetMarkerColorCode(details.DatePosted);

            var bubble = {
                path: 'M 0,0 L0,15 L5,15 L10,20 L15,15 L22,15 L22,0 z',
                fillColor: colorCode,
                fillOpacity: 0,
                scale: 1,
                strokeColor: colorCode,
                strokeWeight: 0
            };

            // Check to see if we've already got a Marker object
            var marker = new MarkerWithLabel({
                position: results[0].geometry.location,
                map: map,
                icon: bubble,
                draggable: false,
                raiseOnDrag: false,
                labelContent: details.CompanyName,
                labelClass: "bubble " + colorCode, // the CSS class for the label
                labelInBackground: true,
                name: details.CompanyName,
                address: details.Location,
                datePosted: details.DatePosted,
                jobId: details.Id
                //MapID: dynamicId
            });

            dynamicId++;

            markerList.push(marker);

            bounds.extend(results[0].geometry.location);

            var maximumZoomLevel = 14;
            var minimumZoomLevel = 11;
            var ourZoom = 14; // default zoom level

            google.maps.event.addListener(marker, 'click', function () {
                map.panTo(marker.getPosition());

                var getMarkerName = this.name;
                var getMarkerLabelContent = this.labelContent;

                var sethtml = '<img src="static/img/close.png" onclick="hide_info_div()" width="15" style="position:absolute;right:0;top:0px;cursor:pointer;" align="">';
                sethtml += '<table cellspacing="5" cellpadding="5">';
                sethtml += '<tr><td>Name</td><td>' + this.name + '</td></tr>';
                sethtml += '<tr><td>Address</td><td>' + wordwrap(this.address, 30, "<br/>\n", true) + '</td></tr>';
                sethtml += '<tr><td>Position</td><td>' + this.getPosition().lat() + ',' + this.getPosition().lng() + '</td></tr>';
                sethtml += '<tr><td>Date Posted</td><td>' + this.datePosted + '</td></tr>';
                sethtml += '</table>';


                //$('#information_div').show().html(sethtml);
                if (previous_val == '') {
                    //$('#information_div').show().html(sethtml);
                    GetJobDetails(this.jobId);
                    $('div.bubble').each(function () {
                        var getText = $(this).text();

                        if (getText == getMarkerName || getText == getMarkerLabelContent) {
                            previous_val = $(this);
                            $('#information_div').show().html(sethtml)
                            $(this).effect('transfer', { to: $('#information_div') }, 400, callback);
                            return false;
                        }
                    });
                    return false;
                }

                //$('#information_div').effect('transfer', { to: previous_val }, 400, callback );

                function callback() {
                    //setTimeout(function() { $('#information_div').show().html(sethtml); }, 400 );
                };

                $('div.bubble').each(function () {
                    var getText = $(this).text();
                    if (getText == getMarkerName || getText == getMarkerLabelContent) {
                        previous_val = $(this);
                        $('#information_div').show().html(sethtml)
                        $(this).effect('transfer', { to: $('#information_div') }, 400, callback);
                    }
                });
            });

            google.maps.event.addListener(marker, "mouseover", function (e) {
                var getContent = marker.name;
                marker.setOptions({ labelContent: marker.name });
            });

            google.maps.event.addListener(marker, "mouseout", function (e) {
                var getContent = marker.name;
                marker.setOptions({ labelContent: getContent });
            });


            //If all markers have been created.
            //if (geocoderRequest.length == markerList.length) {
            //map.fitBounds(bounds);
            //}
        }
    });
}

//function hide_info_div() {
//    $('#information_div').effect('transfer', { to: previous_val }, 200, callback);
//    function callback() {
//        setTimeout(function () { $('#information_div').hide().html(''); }, 200);
//    };
//    previous_val = '';
//}

function chkPointsWithinCircle() {
    var getCircleCenter = setDistanceWidget.get('position');
    var getCircleRadius = setDistanceWidget.get('distance');

    for (var i = 0; i < markerList.length; i++) {
        var markerLoc = markerList[i].getPosition();
        var distance = google.maps.geometry.spherical.computeDistanceBetween(getCircleCenter, markerLoc) / 1000;
        if (parseFloat(distance) <= parseFloat(getCircleRadius)) {
            markerList[i].setVisible(true);
        } else {
            markerList[i].setVisible(false);
        }
    }
}


function wordwrap(str, int_width, str_break, cut) {
    var m = ((arguments.length >= 2) ? arguments[1] : 75);
    var b = ((arguments.length >= 3) ? arguments[2] : "\n"); var c = ((arguments.length >= 4) ? arguments[3] : false);

    var i, j, l, s, r;

    str += '';
    if (m < 1) {
        return str;
    }
    for (i = -1, l = (r = str.split(/\r\n|\n|\r/)).length; ++i < l; r[i] += s) {
        for (s = r[i], r[i] = ""; s.length > m; r[i] += s.slice(0, j) + ((s = s.slice(j)).length ? b : "")) {
            j = c == 2 || (j = s.slice(0, m + 1).match(/\S*(\s)?$/))[1] ? m : j.input.length - j[0].length || c == 1 && m || j.input.length + (j = s.slice(m).match(/^\S*/)).input.length;
        }
    }
    return r.join("\n");
}


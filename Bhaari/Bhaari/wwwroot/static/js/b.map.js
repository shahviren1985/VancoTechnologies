function DistanceWidget(map) {
    this.set('map', map);
    this.set('position', map.getCenter());
}

function RadiusWidget() {
    var circle = new google.maps.Circle({
        strokeWeight: 2, strokeColor: 'rgb(50, 153, 187)',
        strokeOpacity: 0.8,
        fillColor: '#2B60DE',
        fillOpacity: 0.10
    });

    // Set the distance property value, default to 50km.
    this.set('distance', 1.7);

    this.bindTo('bounds', circle);
    circle.bindTo('center', this);
    circle.bindTo('map', this);
    circle.bindTo('radius', this);

    this.addSizer_();
}




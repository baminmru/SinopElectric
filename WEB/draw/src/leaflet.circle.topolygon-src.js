/**
 * @preserve Licensed under MIT License
 */
(function(factory) {
    // Packaging/modules magic dance
    var L;
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(['leaflet'], factory);
    } else if (typeof module !== 'undefined') {
        // Node/CommonJS
        L = require('leaflet');
        module.exports = factory(L);
    } else {
        // Browser globals
        if (typeof window.L === 'undefined') {
            throw new Error('Leaflet must be loaded first');
        }
        factory(window.L);
    }
})(function(L) {
    'use strict';

    /**
     * @const
     * @type {Number}
     */
    L.Circle.SECTIONS_COUNT = 64;

    /**
     * Static
     * @param  {L.Circle} circle
     * @param  {Number?}  vertices
     * @param  {L.Map?}   map
     * @return {Array.<L.LatLng>}
     */
    L.Circle.toPolygon = function(circle, vertices, map) {
        map = map || circle._map;
        if (!map) {
            throw Error("Can't figure out points without adding the feature to the map");
        }

		
        var points = [],
            crs = map.options.crs,
            DOUBLE_PI = Math.PI * 2,
            angle = 0.0,
            projectedCentroid, radius,
            point, project, unproject;
		    var ll0,ll1,dist;
			ll0= circle._latlng;
			ll1= new L.latLng( circle._latlng.lat,circle._latlng.lng+0.01 );
			dist=ll0.distanceTo(ll1);
			if (dist==0) {
				alert("Error dist is 0");
				return null;
			}

      /* if (crs === L.CRS.EPSG3857) {
            project = map.latLngToLayerPoint.bind(map);
            unproject = map.layerPointToLatLng.bind(map);
            radius = circle._mRadius;
        } else { // especially if we are using Proj4Leaflet
            project = crs.projection.project.bind(crs.projection);
            unproject = crs.projection.unproject.bind(crs.projection);
            radius = circle._mRadius;
        }
		

       projectedCentroid = project(circle._latlng);
	  
	  */
	    radius=circle.getRadius() / dist * 0.01;
		if(radius==0) radius = 0.1;
        vertices = vertices || L.Circle.SECTIONS_COUNT;

        for (var i = 0; i < vertices - 1; i++) {
            angle -= (DOUBLE_PI / vertices); // clockwise
            point =new  L.latLng(
			   circle._latlng.lat + (radius * Math.cos(angle)),
			    circle._latlng.lng + (radius * Math.sin(angle)),    
               0
            );
            /*if (i > 0 && point.equals(points[i - 1])) {
                continue;
            }*/
            points.push(point);
        }

        return points;
    };

    /**
     * As a method
     * @param  {Number?} vertices
     * @param  {L.Map?}  map
     * @return {Array.<L.LatLng>}
     */
    L.Circle.prototype.toPolygon = function(vertices, map) {
        return L.Circle.toPolygon(this, vertices, map || this._map);
    };

});

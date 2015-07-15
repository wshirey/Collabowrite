$(document).ready(function () {
    var canvas = new fabric.Canvas('drawingArea', {
        isDrawingMode: true
    });
    canvas.width = document.body.clientWidth;
    canvas.height = document.body.clientHeight;

    var draw = $.connection.drawHub;
    draw.client.draw = function (json) {
        //canvas.add(JSON.parse(json));
    };
    $.connection.hub.start().done(function () {
        canvas.on('path:created', function (object) {
            var json = JSON.stringify(object);
            draw.server.draw(JSON.stringify(object));
        });
    });
});

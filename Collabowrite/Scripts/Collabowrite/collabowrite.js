$(document).ready(function () {
    var canvas = new fabric.Canvas('drawingArea', {
        isDrawingMode: true
    });
    canvas.width = document.body.clientWidth;
    canvas.height = document.body.clientHeight;

    var draw = $.connection.drawHub;
    var send = true;
    draw.client.drawObject = function (json) {
        //canvas.add(JSON.parse(json));
        var object = JSON.parse(json);
        fabric.util.enlivenObjects([JSON.parse(json)], function (objects) {
            send = false;
            var origRenderOnAddRemove = canvas.renderOnAddRemove;
            canvas.renderOnAddRemove = false;

            canvas.add(objects[0]);
            canvas.renderOnAddRemove = origRenderOnAddRemove;
            canvas.renderAll();
            send = true;
        });
    };
    $.connection.hub.start().done(function () {
        canvas.on('object:added', function (object) {
            var json = JSON.stringify(object.target);
            if (send) draw.server.objectAdded(JSON.stringify(object.target));
        });
    });
});

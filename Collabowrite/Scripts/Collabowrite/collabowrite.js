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
        var objects = [];
        if (object instanceof Array === false) {
            objects = [object];
        } else {
            if (object.length == 0) return;
            for (var i = 0; i < object.length; i++)
            {
                objects.push(JSON.parse(object[i]));
            }
        }
        fabric.util.enlivenObjects(objects, function (objects) {
            send = false;
            var origRenderOnAddRemove = canvas.renderOnAddRemove;
            canvas.renderOnAddRemove = false;

            for (var i = 0; i < objects.length; i++) {
                canvas.add(objects[i]);
            }
            canvas.renderOnAddRemove = origRenderOnAddRemove;
            canvas.renderAll();
            send = true;
        });
    };
    $.connection.hub.start().done(function () {
        draw.server.loadObjects();
        canvas.on('object:added', function (object) {
            var json = JSON.stringify(object.target);
            if (send) draw.server.objectAdded(JSON.stringify(object.target));
        });
    });
});

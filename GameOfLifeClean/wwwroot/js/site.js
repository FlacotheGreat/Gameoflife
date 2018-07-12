var gameWidth = 16;
var gameHeight = 16;

var totalHeight = 600;
var totalWidth = 600;

var connection;
var user;
var users = [];
var block;
var startGame;


function drawGrid() {

    user = new user();

    connection = new WebSocketManager.Connection("ws://localhost:5000/server");

    connection.connectionMethods.onConnected = () => {
        user.id = connection.connectionId;
                          //("Name of Method to invoke on server",SocketID,Content to pass)
        connection.invoke("ConnectedUser", connection.connectionId, JSON.stringify(user));

    }

    connection.connectionMethods.onDisconnected = () => {

        connection.invoke("DisconnectedUser", connection.connectionId,"");

    }

    connection.clientMethods["pingUsers"] = (serUsers) => {
        users = JSON.parse(serUser);
        console.log(users);
    };

    connection.clientMethods["ReceiveUpdateAsXYColor"] = (socketId, x, y, fillColor) => {
        assignColorToSquare(x,y,fillColor);
    };

    connection.start()

    $(window).unload(function () {
        connection.invoke("DisconnectedUser", connection.connectionId, "");
    });

    var c = document.getElementById("gameOfLife");
    var ctx = c.getContext("2d");
    ctx.clearRect(0, 0, totalWidth, totalHeight); // clear the canvass
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, totalWidth, totalHeight);
    ctx.stroke();
    for (var i = 0; i < gameWidth; i++) {
        for (var j = 0; j < gameHeight; j++) {
            // console.log(((totalWidth/gameWidth)*i) + ", " + ((totalHeight/gameHeight)*j));
            ctx.fillStyle = "#FFFFFF";
            ctx.fillRect(((totalWidth / gameWidth) * i), ((totalHeight / gameHeight) * j), (totalWidth / gameWidth) - 1, (totalHeight / gameHeight) - 1);
            ctx.stroke();
        }
    }

    c.onmouseup = function (e) {
        var rect = e.target.getBoundingClientRect();
        // x = (2u - w) / w
        var x = (2 * (e.clientX - rect.left) - rect.width) / rect.width;
        // y = (h - 2v) / h
        var y = (rect.height - 2 * (e.clientY - rect.top)) / rect.height;
        // console.log(x, y);
        var xCell = 0;

        for(var i = -0.875; i < x; i += 0.125){
          xCell++;
        }

        var yCell = 15;
        for(var i = -0.875; i < y; i += 0.125){
          yCell--;
        }

        console.log("x: " + xCell + " y: " + yCell);
        //send x and y cell data to c#

        assignColorToSquare(xCell, yCell);

        connection.invoke("PassXandY", connection.connectionId, JSON.stringify(xCell), JSON.stringify(yCell), document.getElementById("userColor").value);

    }
}

function user() {
    this.id = "";
    this.color = "";
}

function block() {
    this.x = "";
    this.y = "";
    this.alive = "";
}

function assignColorToSquare(x, y, color) {
    var c = document.getElementById("gameOfLife");
    var ctx = c.getContext("2d");
    ctx.fillStyle = color;
    ctx.fillRect((totalWidth / gameWidth) * x, ((totalHeight / gameHeight) * y), (totalWidth / gameWidth) - 1, (totalHeight / gameHeight) - 1);
    ctx.stroke();
}

function assignColorToSquare(x, y) {
    var c = document.getElementById("gameOfLife");
    var ctx = c.getContext("2d");
    ctx.fillStyle = document.getElementById("userColor").value;
    console.log(document.getElementById("userColor").value);
    ctx.fillRect((totalWidth / gameWidth) * x, ((totalHeight / gameHeight) * y), (totalWidth / gameWidth) - 1, (totalHeight / gameHeight) - 1);
    ctx.stroke();
}

function clearSquare(x, y) {
    var c = document.getElementById("gameOfLife");
    var ctx = c.getContext("2d");
    ctx.fillStyle = "#FFFFFF";
    ctx.fillRect((totalWidth / gameWidth) * x, ((totalHeight / gameHeight) * y), (totalWidth / gameWidth) - 1, (totalHeight / gameHeight) - 1);
    ctx.stroke();
}

function Start(){
    startGame = true;
    connection.invoke("startGame", connection.connectionId, JSON.stringify(startGame));
}
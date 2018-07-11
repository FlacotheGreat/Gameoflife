var gameWidth = 16;
var gameHeight = 16;

var totalHeight = 600;
var totalWidth = 600;

var connection;
var user;
var users = [];
var block;


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

    connection.clientMethods["recieveNewBoard"] = (socketId,  ) => {
        users = JSON.parse(serUser);
        console.log(users);
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
        connection.invoke("PassXandY", connection.connectionId, JSON.stringify(xCell), JSON.stringify(yCell));
        assignColorToSquare(xCell, yCell);

    }
}



//document.getElementById("Start").onclick = function (e) { connection.invoke("startGame", connection.connectionId, "True"); 

function Start() {
    connection.invoke("startGame", connection.connectionId, "True");
    console.log("Game Started");
}

function Stop() {
    connection.invoke("startGame", connection.connectionId, "False");
    console.log("Game Stoped");
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

function startGame() {

    // var xmlhttp = new XMLHttpRequest();
    // xmlhttp.onreadystatechange = function() {
    //     if (this.readyState == 4 && this.status == 200) {
    //         document.getElementById("txtHint").innerHTML = this.responseText;
    //     }
    // };
    // xmlhttp.open("GET", "LogicController.asp?x=" + xCell + "y" + yCell, true);
    // xmlhttp.send();

    // $.ajax({
    //     type: "POST",
    //     url: 'GameManager.cs/GameManager.Instance.Initialize()',
    //     data: "",
    //     contentType: "application/json; charset=utf-8",
    //     dataType: "json",
    //     success: function (msg){
    //         $("#divResult").html("success");
    //     },
    //     error: function(e){
    //         $("#divResult").html("Something bad happened.");
    //     }

    // });
}

function stopGame() {
    $.ajax({
        type: "POST",
        url: 'GameManager.cs/GameManager.Instance.StopTime()',
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg){
            $("#divResult").html("success");
        },
        error: function(e){
            $("#divResult").html("Something bad happened.");
        }

    });
}
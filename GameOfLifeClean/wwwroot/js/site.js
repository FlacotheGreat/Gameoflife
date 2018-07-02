var gameWidth = 16;
var gameHeight = 16;

var totalHeight = 600;
var totalWidth = 600;

var connection;
var user;
var users[];
var block;

function drawGrid() {

    user = new user();

    connection = new WebSocketManager.Connection("ws://localhost:5000/server");

    connection.connectionMethods.onConnected = () => {
        user.id = connection.connectionId;

        connection.invoke("ConnectedUser", connection.connectionId, JSON.stringify(user));

    }

    connection.connectionMethods.onDisconnected = () => {

        connection.invoke("DisconnectedUser", connection.connectionId,"");

    }

    connection.clientMethods["pingUsers"] = (serUsers) => {
        users = JSON.parse(serUser);
        console.log(users);
    };

    connection.start()

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
        var xCell;

        if (x > -1 && x < -0.875) {
            xCell = 0;
        } else if (x > -0.875 && x < -0.75) {
            xCell = 1;
        } else if (x > -0.75 && x < -0.625) {
            xCell = 2;
        } else if (x > -0.625 && x < -0.5) {
            xCell = 3;
        } else if (x > -0.5 && x < -0.375) {
            xCell = 4;
        } else if (x > -0.375 && x < -0.25) {
            xCell = 5;
        } else if (x > -0.25 && x < -0.125) {
            xCell = 6;
        } else if (x > -0.125 && x < 0) {
            xCell = 7;
        } else if (x > 0 && x < 0.125) {
            xCell = 8;
        } else if (x > 0.125 && x < 0.25) {
            xCell = 9;
        } else if (x > 0.25 && x < 0.375) {
            xCell = 10;
        } else if (x > 0.375 && x < 0.5) {
            xCell = 11;
        } else if (x > 0.5 && x < 0.625) {
            xCell = 12;
        } else if (x > 0.625 && x < 0.75) {
            xCell = 13;
        } else if (x > 0.75 && x < 0.875) {
            xCell = 14;
        } else if (x > 0.875 && x < 1.0) {
            xCell = 15;
        }

        if (y > -1 && y < -0.875) {
            yCell = 15;
        } else if (y > -0.875 && y < -0.75) {
            yCell = 14;
        } else if (y > -0.75 && y < -0.625) {
            yCell = 13;
        } else if (y > -0.625 && y < -0.5) {
            yCell = 12;
        } else if (y > -0.5 && y < -0.375) {
            yCell = 11;
        } else if (y > -0.375 && y < -0.25) {
            yCell = 10;
        } else if (y > -0.25 && y < -0.125) {
            yCell = 9;
        } else if (y > -0.125 && y < 0) {
            yCell = 8;
        } else if (y > 0 && y < 0.125) {
            yCell = 7;
        } else if (y > 0.125 && y < 0.25) {
            yCell = 6;
        } else if (y > 0.25 && y < 0.375) {
            yCell = 5;
        } else if (y > 0.375 && y < 0.5) {
            yCell = 4;
        } else if (y > 0.5 && y < 0.625) {
            yCell = 3;
        } else if (y > 0.625 && y < 0.75) {
            yCell = 2;
        } else if (y > 0.75 && y < 0.875) {
            yCell = 1;
        } else if (y > 0.875 && y < 1.0) {
            yCell = 0;
        }

<<<<<<< HEAD
        // if (x > -1 && x < -0.875) {
        //     xCell = 0;
        // } else if (x > -0.875 && x < -0.75) {
        //     xCell = 1;
        // } else if (x > -0.75 && x < -0.625) {
        //     xCell = 2;
        // } else if (x > -0.625 && x < -0.5) {
        //     xCell = 3;
        // } else if (x > -0.5 && x < -0.375) {
        //     xCell = 4;
        // } else if (x > -0.375 && x < -0.25) {
        //     xCell = 5;
        // } else if (x > -0.25 && x < -0.125) {
        //     xCell = 6;
        // } else if (x > -0.125 && x < 0) {
        //     xCell = 7;
        // } else if (x > 0 && x < 0.125) {
        //     xCell = 8;
        // } else if (x > 0.125 && x < 0.25) {
        //     xCell = 9;
        // } else if (x > 0.25 && x < 0.375) {
        //     xCell = 10;
        // } else if (x > 0.375 && x < 0.5) {
        //     xCell = 11;
        // } else if (x > 0.5 && x < 0.625) {
        //     xCell = 12;
        // } else if (x > 0.625 && x < 0.75) {
        //     xCell = 13;
        // } else if (x > 0.75 && x < 0.875) {
        //     xCell = 14;
        // } else if (x > 0.875 && x < 1.0) {
        //     xCell = 15;
        // }
        //
        // if (y > -1 && y < -0.875) {
        //     yCell = 15;
        // } else if (y > -0.875 && y < -0.75) {
        //     yCell = 14;
        // } else if (y > -0.75 && y < -0.625) {
        //     yCell = 13;
        // } else if (y > -0.625 && y < -0.5) {
        //     yCell = 12;
        // } else if (y > -0.5 && y < -0.375) {
        //     yCell = 11;
        // } else if (y > -0.375 && y < -0.25) {
        //     yCell = 10;
        // } else if (y > -0.25 && y < -0.125) {
        //     yCell = 9;
        // } else if (y > -0.125 && y < 0) {
        //     yCell = 8;
        // } else if (y > 0 && y < 0.125) {
        //     yCell = 7;
        // } else if (y > 0.125 && y < 0.25) {
        //     yCell = 6;
        // } else if (y > 0.25 && y < 0.375) {
        //     yCell = 5;
        // } else if (y > 0.375 && y < 0.5) {
        //     yCell = 4;
        // } else if (y > 0.5 && y < 0.625) {
        //     yCell = 3;
        // } else if (y > 0.625 && y < 0.75) {
        //     yCell = 2;
        // } else if (y > 0.75 && y < 0.875) {
        //     yCell = 1;
        // } else if (y > 0.875 && y < 1.0) {
        //     yCell = 0;
        // }

        // send data to c sharp code
        // var postdata = JSON.stringify(
        //  {
        //      "x": xCell,
        //      "y": yCell,
        //  });
        //  try {
        //      $.ajax({
        //          type: "POST",
        //          url: "",
        //          cache: false,
        //          data: postdata,
        //          dataType: "json",
        //          success: getSuccess,
        //          error: getFail
        //      });
        //  } catch (e) {
        //      alert(e);
        //  }
        //  function getSuccess(data, textStatus, jqXHR) {
        //      alert(data.Response);
        //  };
        //  function getFail(jqXHR, textStatus, errorThrown) {
        //      alert(jqXHR.status);
        //  };
        //
        // // console.log(xCell, yCell);
        // assignColorToSquare(xCell, yCell, "#0000FF");
=======
        // console.log(xCell, yCell);
        assignColorToSquare(xCell, yCell, "#0000FF");
>>>>>>> AnthonyBranch
    }
}

$(window).unload =(function () {
    connection.invoke("DisconnectedUser", connection.connectionId,"")
})

function user() {
    this.id = "";
    this.color = "";
}

function block() {
    this.id = "";
    this.color = user.color;
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
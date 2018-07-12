using GameOfLifeClean.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using TestASPWebApplicationMVC;
using WebSocketManager;
using WebSocketManager.Common;

namespace GameOfLifeClean
{
    public class GameHandler : WebSocketHandler
    {
        private bool isStarted = false;
        GameLogic game = new GameLogic();

        public GameHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

     
        //Used to unique users using websockets
        public async Task ConnectedUser(string socketId, string serializedUser)
        {

            var user = JsonConvert.DeserializeObject<User>(serializedUser);

            var exists = GameManager.Instance.Users.ContainsKey(socketId);
            if (!exists)
            {
                GameManager.Instance.Users.TryAdd(user.Id, user);
            }
        }

        //Disconnects the user
        public async Task DisconnectedUser(string socketId, string usr)
        {
            GameManager.Instance.Users.TryRemove(socketId, out User usrname);
        }


        //When new user connects a new socketID is created 
        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            //this will be used to identify the user and their color
            var socketID = WebSocketConnectionManager.GetId(socket);

            var message = new Message
            {
                MessageType = MessageType.Text,
                Data = $"User: {socketID} is Connected"

            };

            await SendMessageToAllAsync(message);
            Console.WriteLine(message.Data);
        }

        //Disconnects the user with the socketID passed in
        public override async Task OnDisconnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketID = WebSocketConnectionManager.GetId(socket);

            var message = new Message
            {
                MessageType = MessageType.Text,
                Data = $"User: {socketID} is now disconnected"
            };

            await SendMessageToAllAsync(message);
            Console.WriteLine(message.Data);
        }

        //Gets X and Y coords from Canvas element and passes it to the game logic
        public async Task PassXandY(string socketId, string X, string Y, string userColor)
        {
            int yCoord = Convert.ToInt16(JsonConvert.DeserializeObject(Y));
            var xCoord = Convert.ToInt16(JsonConvert.DeserializeObject(X));

            System.Drawing.Color fillColor = System.Drawing.Color.FromName(userColor);

            Console.WriteLine(xCoord + ":" + yCoord);

            game.onNewClick(xCoord, yCoord, fillColor);

            await InvokeClientMethodToAllAsync("ReceiveUpdateAsXYColor", socketId, xCoord, yCoord, fillColor.R.ToString("X"), fillColor.G.ToString("X"), fillColor.B.ToString("X"));
                
            Console.WriteLine("");
            
        }

        // public async Task SendXYColor(string socketId, string x, string y, string red, string green, string blue)
        // {

        //     await InvokeClientMethodToAllAsync("ReceiveUpdateAsXYColor", socketId, x, y, red, green, blue);
        // }

        public async Task startGame(string socketId, string isStart)
        {
            isStarted = Convert.ToBoolean(isStart);
            //Timer = new System.Timers.Timer(2000);
                game.getNextGrid();

                for(int i = 0; i < 16; i++){
                    for(int j = 0; j < 16; j++){
                        Console.WriteLine("x: " + i + " y: " + j + " color: " + game.getIndexColor(i,j) + " alive: " + game.checkNeighbor(i,j));
                        await InvokeClientMethodToAllAsync("ReceiveUpdateAsXYColor", socketId, i, j, game.getRed(i,j).ToString("X"), game.getGreen(i,j).ToString("X"), game.getBlue(i,j).ToString("X"));
                    }

                    Console.WriteLine("");
                }
                Console.WriteLine(isStarted);
            
        }

    }
}

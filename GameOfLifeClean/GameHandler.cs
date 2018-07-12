using GameOfLifeClean.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Timers;
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
            int xCoord = Convert.ToInt16(JsonConvert.DeserializeObject(X));

            //chops off the hash
            string colorSubstring = userColor.Substring(1);
            // Console.WriteLine("r substring: " + colorSubstring.Substring(0, 2));
            int rVal = int.Parse(colorSubstring.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int gVal = int.Parse(colorSubstring.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int bVal = int.Parse(colorSubstring.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            // Console.WriteLine("Color: " + rVal + gVal + bVal);
            System.Drawing.Color fillColor = System.Drawing.Color.FromArgb(rVal, gVal, bVal);

            Console.WriteLine(xCoord + ":" + yCoord + " " + fillColor);

            game.onNewClick(xCoord, yCoord, fillColor);

            //if the game is paused, then send updates to all users, either giving the color and the cell, or setting the cell to white
            if(!isStarted){
                if(game.checkNeighbor(xCoord, yCoord)){
                    await InvokeClientMethodToAllAsync("ReceiveUpdateAsXYColor", socketId, xCoord, yCoord, fillColor.R.ToString("X").PadLeft(2, '0'), fillColor.G.ToString("X").PadLeft(2, '0'), fillColor.B.ToString("X").PadLeft(2, '0'));
                }
                else{
                    await InvokeClientMethodToAllAsync("ReceiveUpdateAsXYColor", socketId, xCoord, yCoord, "FF", "FF", "FF");
                }
                // await InvokeClientMethodToAllAsync("ReceiveUpdateAsXYColor", socketId, xCoord, yCoord, fillColor.R.ToString("X").PadLeft(2, '0'), fillColor.G.ToString("X").PadLeft(2, '0'), fillColor.B.ToString("X").PadLeft(2, '0'));
            }
                
            Console.WriteLine("");
            
        }

        public async Task startGame(string socketId, string isStart)
        {
            isStarted = Convert.ToBoolean(isStart);
            while(isStarted){      
                System.Timers.Timer gameTimer = new System.Timers.Timer(1000);
                gameTimer.Elapsed += new ElapsedEventHandler(generateAndSendNewGrids);
                gameTimer.Start();
                // game.getNextGrid();
                
                // // System.Threading.Thread.Sleep(1000/15); 

                // // Console.WriteLine("Sending the following to the users:");
                // for(int i = 0; i < 16; i++){
                //     for(int j = 0; j < 16; j++){
                //         // Console.WriteLine("x: " + i + " y: " + j + " color: " + game.getIndexColor(i,j) + " alive: " + game.checkNeighbor(i,j));
                //         await InvokeClientMethodToAllAsync("ReceiveUpdateAsXYColor", socketId, i, j, game.getRed(i,j).ToString("X").PadLeft(2, '0'), game.getGreen(i,j).ToString("X").PadLeft(2, '0'), game.getBlue(i,j).ToString("X").PadLeft(2, '0'));
                //     }

                //     // Console.WriteLine("");
                // }
                // Console.WriteLine(isStarted);
            }
            
        }

        public async void generateAndSendNewGrids(object source, ElapsedEventArgs e){
            Console.WriteLine(GameManager.Instance.Users.Keys.Count);
            while(isStarted){
                game.getNextGrid();
                for(int i = 0; i < 16; i++){
                        for(int j = 0; j < 16; j++){
                            //needs to send to each user, somehow getting the socketID...
                            foreach (string someSocketId in GameManager.Instance.Users.Keys){
                                // Console.WriteLine(someSocketId);
                                await InvokeClientMethodToAllAsync("ReceiveUpdateAsXYColor", someSocketId, i, j, game.getRed(i,j).ToString("X").PadLeft(2, '0'), game.getGreen(i,j).ToString("X").PadLeft(2, '0'), game.getBlue(i,j).ToString("X").PadLeft(2, '0'));
                            }
                            // Console.WriteLine("x: " + i + " y: " + j + " color: " + game.getIndexColor(i,j) + " alive: " + game.checkNeighbor(i,j));
                            
                        }

                        // Console.WriteLine("");
                }
                Console.WriteLine(isStarted);
            }
        }

        public async Task stopGame(string socketId, string isStart){
            isStarted = Convert.ToBoolean(isStart);
        }

    }
}

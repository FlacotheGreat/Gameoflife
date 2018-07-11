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
    public class UserHandler : WebSocketHandler
    {
        public UserHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public async Task ConnectedUser(string socketId, string serializedUser)
        {
            var user = JsonConvert.DeserializeObject<User>(serializedUser);
            

            var exists = GameManager.Instance.Users.ContainsKey(socketId);
            if (!exists)
            {
                GameManager.Instance.Users.TryAdd(user.Id, user);
            }
        }

        public async Task DisconnectedUser(string socketId, string usr)
        {
            GameManager.Instance.Users.TryRemove(socketId, out User usrname);
        }

        public async Task PassXandY(string socketId, string X, string Y)
        {
            int yCoord = Convert.ToInt16(JsonConvert.DeserializeObject(Y));
            var xCoord = Convert.ToInt16(JsonConvert.DeserializeObject(X));

            Block b = new Block();
            GameLogic game = new GameLogic();

            Console.WriteLine(xCoord + ":" + yCoord);

            game.onNewClick(xCoord, yCoord);

            //b.x = block.x;
            //b.y = block.y;
            //b.IsAlive = true;
        }

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
            Console.WriteLine(message.Data);
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketID = WebSocketConnectionManager.GetId(socket);

            var message = new Message
            {
                MessageType = MessageType.Text,
                Data = $"User: {socketID} is now disconnected"
            };
            Console.WriteLine(message.Data);

        }
    }
}

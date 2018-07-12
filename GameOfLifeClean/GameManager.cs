using GameOfLifeClean;
using GameOfLifeClean.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Extensions.DependencyInjection;


namespace TestASPWebApplicationMVC
{
    public class GameManager
    {
        private static GameManager instance;
        private static readonly object padLock = new object();
        public ConcurrentDictionary<string, User> Users { get; set; }

        public static GameManager Instance
        {
            get
            {
                lock (padLock)
                {
                    if(instance == null)
                    {
                        instance = new GameManager();
                    }
                    return instance;
                }
            }
        }

        public void Initialize()
        {
            Users = new ConcurrentDictionary<string, User>();
        }

        private void callback(object state)
        {

            var listOfUsers = JsonConvert.SerializeObject(Users.Values);
            //Send the users to the open client


            //foreach (var item in Users.Values)
            //{
            //    Console.WriteLine(item.Id + "-" + item.Color);
            //}
            //Startup.ServiceProvider
            //.GetRequiredService<UserHandler>()
            //.InvokeClientMethodToAllAsync("pingUsers", listOfUsers)
            //.Wait();

        }
    }
}
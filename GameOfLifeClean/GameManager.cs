using GameOfLifeClean.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;


namespace TestASPWebApplicationMVC
{
    public class GameManager
    {
        private static GameManager instance;
        private static readonly object padLock = new object();
        public ConcurrentDictionary<string, Block> Blocks { get; set; }
        public Timer Timer;

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
            Blocks = new ConcurrentDictionary<string, Block>();
            Timer = new Timer(callback, null,0, 1000/15);
        }

        private void callback(object state)
        {
            var listOfBlocks = JsonConvert.SerializeObject(Blocks.Values);
            //Send the blocks to the open client
        }
    }
}
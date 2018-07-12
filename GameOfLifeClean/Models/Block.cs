using System;
using System.Collections.Generic;

namespace GameOfLifeClean.Models
{
    public partial class Block
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool IsAlive { get; set; }

        public System.Drawing.Color blockColor { get; set; }
        public uint id { get; set; }
        //server id
    }

}

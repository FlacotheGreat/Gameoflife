using System;
using System.Collections.Generic;

namespace GameOfLifeClean.Models
{
    public partial class Block
    {
        public short? x { get; set; }
        public short? y { get; set; }
        public sbyte? IsAlive { get; set; }
        public uint id { get; set; }
        //server id
    }

    //public Block GetBlock(String blockInfo)
    //{
    //    Block B = new Block();

    //    return B;
    //}
}

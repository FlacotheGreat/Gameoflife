using System;
using System.Collections.Generic;

namespace GameOfLifeClean.Models
{
    public partial class Block
    {
        public short? X { get; set; }
        public short? Y { get; set; }
        public sbyte? IsAlive { get; set; }
        public uint Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Mines
{
    class Case
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

            // Sum of the mine number (-1 if mine)
        public int Value { get; set; }
        public bool IsMine { get; set; }
        public bool Exposed { get; set; }
    }
}

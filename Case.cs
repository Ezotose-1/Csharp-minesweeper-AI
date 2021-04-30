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
            // Exposed = already click by user
        public bool Exposed { get; set; }
            // Tagged by player = it's a bomb
        public bool IsTagged { get; set; }
    }
}

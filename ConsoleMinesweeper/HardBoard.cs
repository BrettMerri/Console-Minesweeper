using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class HardBoard : Board
    {
        public HardBoard()
        {
            Title = "Hard";

            Horizontal = 16;
            Vertical = 30;

            Mines = 99;

            HasMineBoardArray = new bool[Horizontal, Vertical];
            SurroundingMinesArray = new int[Horizontal, Vertical];
        }
    }
}

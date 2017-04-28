using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class CustomBoard : Board
    {
        public CustomBoard(int horizontal, int verical, int mines)
        {
            Title = "Custom";
            Horizontal = horizontal;
            Vertical = verical;
            Mines = mines;
            HasMineBoardArray = new bool[Horizontal, Vertical];
            SurroundingMinesArray = new int[Horizontal, Vertical];
        }
    }
}

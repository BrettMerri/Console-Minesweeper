using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class EasyBoard : Board
    {
        public EasyBoard()
        {
            Title = "Easy";
            Horizontal = 9;
            Vertical = 9;
            Mines = 10;
            HasMineBoardArray = new bool[Horizontal, Vertical];
            SurroundingMinesArray = new int[Horizontal, Vertical];
        }
    }
}

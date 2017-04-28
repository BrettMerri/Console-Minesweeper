using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class MediumBoard : Board
    {
        public MediumBoard()
        {
            Title = "Medium";
            Horizontal = 16;
            Vertical = 16;
            Mines = 40;
            HasMineBoardArray = new bool[Horizontal, Vertical];
        }
    }
}


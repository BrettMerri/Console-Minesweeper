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

            TwoDigitYAxis = true;
            TwoDigitXAxis = true;

            TotalMines = 99;

            CreateEmptyCellArray();
        }
    }
}

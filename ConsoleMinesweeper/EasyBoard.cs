using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class EasyBoard : Board
    {
        public EasyBoard() : base()
        {
            Title = "Easy";

            Horizontal = 9;
            Vertical = 9;

            TwoDigitYAxis = false;
            TwoDigitXAxis = false;

            TotalMines = 10;

            CreateEmptyCellArray();
        }
    }
}

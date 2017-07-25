using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class CustomBoard : Board
    {
        public CustomBoard(int horizontal, int verical, int mines) : base()
        {
            Title = "Custom";

            Horizontal = horizontal;
            Vertical = verical;

            TwoDigitXAxis = Horizontal > 9 ? true : false;
            TwoDigitYAxis = Vertical > 9 ? true : false;

            TotalMines = mines;

            CreateEmptyCellArray();
        }
    }
}

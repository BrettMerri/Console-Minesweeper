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
            Horizontal = 9;
            Vertical = 9;
            Mines = 10;
        }
    }
}

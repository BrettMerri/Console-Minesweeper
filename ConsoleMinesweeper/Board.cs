using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    abstract class Board
    {
        private int horizontal;
        private int vertical;
        private int mines;

        public Board()
        {
            horizontal = 0;
            vertical = 0;
            mines = 0;
        }

        public Board(int horizontal, int vertical, int mines)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.mines = mines;
        }

        public abstract void CreateBoard();

        public int Horizontal
        {
            get
            {
                return horizontal;
            }

            set
            {
                horizontal = value;
            }
        }

        public int Vertical
        {
            get
            {
                return vertical;
            }

            set
            {
                vertical = value;
            }
        }

        public int Mines
        {
            get
            {
                return mines;
            }

            set
            {
                mines = value;
            }
        }
    }
}

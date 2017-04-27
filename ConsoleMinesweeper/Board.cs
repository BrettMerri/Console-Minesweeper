using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class Board
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

        public void CreateBoard()
        {
            int[,] EmptyBoardArray = new int[Horizontal, Vertical];

            int[,] BoardArray = GenerateMines(EmptyBoardArray);

            for (int i = 0; i < Horizontal; i++)
            {
                for (int j = 0; j < Vertical; j++)
                {
                    Console.Write(string.Format("{0} ", BoardArray[i, j]));
                }
                Console.Write("\n");
            }
        }
        public int[,] GenerateMines(int[,] EmptyBoardArray)
        {
            Random r = new Random();

            for (int i = 0; i < Mines; i++)
            {
                if (EmptyBoardArray[r.Next(0, Horizontal), r.Next(0, Vertical)] == 1)
                    i--;
                else
                    EmptyBoardArray[r.Next(0, Horizontal), r.Next(0, Vertical)] = 1;
            }
            return EmptyBoardArray;
        }

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

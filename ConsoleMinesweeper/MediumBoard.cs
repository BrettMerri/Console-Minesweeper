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
            Horizontal = 16;
            Vertical = 16;
            Mines = 40;
        }
        public override void CreateBoard()
        {
            int[,] BoardArray = new int[16, 16];

            Random r = new Random();

            int rowLength = BoardArray.GetLength(0);
            int colLength = BoardArray.GetLength(1);

            for (int i = 0; i < Mines; i++)
            {
                if (BoardArray[r.Next(0, rowLength), r.Next(0, colLength)] == 1)
                    i--;
                else
                    BoardArray[r.Next(0, rowLength), r.Next(0, colLength)] = 1;
            }

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", BoardArray[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

        }
    }
}


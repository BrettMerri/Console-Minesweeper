using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    public class Board
    {
        private Cell[,] cellArray;

        private string title;
        private int horizontal;
        private int vertical;
        private int totalMines;
        private bool twoDigitYAxis;
        private bool twoDigitXAxis;

        public Board()
        {

        }

        protected void CreateEmptyCellArray()
        {
            CellArray = new Cell[Vertical, Horizontal];

            int maxVertical = CellArray.GetLength(0) - 1;
            int maxHorizontal = CellArray.GetLength(1) - 1;

            for (int y = 0; y < CellArray.GetLength(0); y++)
            {
                for (int x = 0; x < CellArray.GetLength(1); x++)
                {
                    CellArray[y, x] = new Cell(y, x, maxVertical, maxHorizontal);
                }
            }
        }

        public void WriteBoard()
        {
            for (int y = 0; y < CellArray.GetLength(0); y++)
            {
                WriteYCoordinate(y);
                for (int x = 0; x < CellArray.GetLength(1); x++)
                {
                    Console.Write("X ");
                    if (TwoDigitXAxis) //If TwoDigitXAxis add extra space to allow room for x coodinates
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            WriteXCoordinates();
        }

        private void WriteYCoordinate(int y)
        {
            int yValue = Vertical - y; //This reverses y and adds 1 so if y is 0 and vertical is 10 then yValue is 10
                                       //if y is 9 (max value for vertical 10) then yValue is 1
            Console.Write(yValue + " ");
            if (TwoDigitYAxis && yValue <= 9)
                Console.Write(" "); //If TwoDigitYAxis and printing single digit, add extra space
        }

        private void WriteXCoordinates()
        {
            Console.Write("  "); //Left padding before X Coodinates start

            if (TwoDigitYAxis)
                Console.Write(" "); //if TwoDigitYAxis add extra space to padding

            for (int i = 0; i < Horizontal; i++)
            {
                Console.Write(i + 1 + " ");

                if (TwoDigitXAxis && i + 1 < 10)
                    Console.Write(" "); //if TwoDigitXAxis and printing a single digit number, add an extra space
            }
            Console.WriteLine();
        }

        public void GenerateMines(int y, int x)
        {
            CellArray[y, x].IsSelected = true;
            int mineCount = 0;

            Random yRnd = new Random();
            Random xRnd = new Random();

            int yRndValue = yRnd.Next(Vertical);
            int xRndValue = xRnd.Next(Horizontal);

            if (CellArray[y, x].CheckAbove &&
                !CellArray[y - 1, x].IsMineBlacklisted)
            {

            }
            
            
        }

        private void BlacklistSurroundingCells(int y, int x)
        {
            if (CellArray[y, x].CheckAbove)
            {
                CellArray[y - 1, x].IsMineBlacklisted = true;
            }
            if (CellArray[y, x].CheckBelow)
            {
                CellArray[y + 1, x].IsMineBlacklisted = true;
            }
            if (CellArray[y, x].CheckLeft)
            {
                CellArray[y, x - 1].IsMineBlacklisted = true;
            }
            if (CellArray[y, x].CheckRight)
            {
                CellArray[y, x + 1].IsMineBlacklisted = true;
            }

        }

        public string Title { get; set; }
        public int Horizontal { get; set; }
        public int Vertical { get; set; }
        public int TotalMines { get; set; }
        public Cell[,] CellArray { get; set; }
        protected bool TwoDigitYAxis { get; set; }
        protected bool TwoDigitXAxis { get; set; }
    }
}

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

            for (int y = 0; y < Vertical; y++)
            {
                for (int x = 0; x < Horizontal; x++)
                {
                    CellArray[y, x] = new Cell();
                }
            }
        }

        public void WriteBoard()
        {
            for (int y = 0; y < Vertical; y++)
            {
                WriteYCoordinate(y);
                for (int x = 0; x < Horizontal; x++)
                {
                    Cell cell = CellArray[y, x];

                    if (cell.IsFlagged)
                        Console.Write("F");
                    else if (!cell.IsSelected)
                        Console.Write("#");
                    else if (cell.IsMine)
                        Console.Write("X");
                    else
                        Console.Write(cell.SurroundingMinesValue);

                    if (TwoDigitXAxis) //If TwoDigitXAxis is true add extra space to allow room for x coodinates
                        Console.Write("  ");
                    else
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

            BlacklistSurroundingCells(y, x);

            Random rnd = new Random();

            for (int i = 0; i < TotalMines; i++)
            {
                int yRndValue = rnd.Next(Vertical);
                int xRndValue = rnd.Next(Horizontal);

                if (!CellArray[yRndValue, xRndValue].IsMineBlacklisted &&
                    !CellArray[yRndValue, xRndValue].IsMine &&
                    !CellArray[yRndValue, xRndValue].IsSelected)
                {
                    CellArray[yRndValue, xRndValue].IsMine = true;
                }
                else
                {
                    i--;
                }
            }

        }

        private void BlacklistSurroundingCells(int yInput, int xInput)
        {
            for (int y = yInput - 1; y <= yInput + 1; y++)
            {
                if (y >= 0 && y < Vertical)
                {
                    for (int x = xInput - 1; x <= xInput + 1; x++)
                    {
                        if (x >= 0 && x < Horizontal)
                        {
                            CellArray[y, x].IsMineBlacklisted = true;
                        }
                    }
                }
            }
        }

        public Cell[,] CellArray { get => cellArray; set => cellArray = value; }
        public string Title { get => title; set => title = value; }
        public int Horizontal { get => horizontal; set => horizontal = value; }
        public int Vertical { get => vertical; set => vertical = value; }
        public int TotalMines { get => totalMines; set => totalMines = value; }
        protected bool TwoDigitYAxis { get => twoDigitYAxis; set => twoDigitYAxis = value; }
        protected bool TwoDigitXAxis { get => twoDigitXAxis; set => twoDigitXAxis = value; }

    }
}

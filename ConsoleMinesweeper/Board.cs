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
        private GameState state;

        public Board()
        {
            State = GameState.BlankGameBoard;
        }

        public void WriteBoard()
        {
            int selectableCells = Horizontal * Vertical;
            //Goes through y values top to bottom
            for (int y = 0; y < Vertical; y++)
            {
                WriteYCoordinate(y);

                //Goes through x values left to right
                for (int x = 0; x < Horizontal; x++)
                {
                    Cell cell = CellArray[y, x];

                    if (cell.IsFlagged)
                    {
                        Program.PrintColoredString("F", ConsoleColor.Green);
                    }
                    else if (!cell.IsSelected)
                    {
                        Program.PrintColoredString("#", ConsoleColor.DarkGray);
                    }
                    else if (cell.IsMine)
                    {
                        Program.PrintColoredString("X", ConsoleColor.Red);
                        State = GameState.MineSelected;
                    }
                    else
                    {
                        WriteColoredSurroundingMinesValue(cell.SurroundingMinesValue);
                        selectableCells--;
                    }

                    if (TwoDigitXAxis) //If TwoDigitXAxis is true add extra space to allow room for x coodinates
                        Console.Write("  ");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            WriteXCoordinates();

            //Checks if the game has been won
            if (selectableCells == TotalMines && State != GameState.MineSelected)
            {
                State = GameState.GameWon;
            }
        }

        public void SelectCell(int y, int x)
        {
            if (!CellArray[y, x].IsSelected &&
                !CellArray[y, x].IsFlagged)
            {
                CellArray[y, x].IsSelected = true;

                if (State == GameState.BlankGameBoard)
                {
                    GenerateMines(y, x);
                    State = GameState.GameInProgress;
                }

                if (CellArray[y, x].SurroundingMinesValue == 0)
                {
                    RevealAroundConnectingZeros(y, x);
                }
            }
        }

        public void FlagCell(int y, int x)
        {
            if (!CellArray[y, x].IsSelected)
            {
                if (!CellArray[y, x].IsFlagged)
                    CellArray[y, x].IsFlagged = true;
                else
                    CellArray[y, x].IsFlagged = false;
            }
        }

        private void RevealAroundConnectingZeros(int yInput, int xInput)
        {
            //This is set to true to indicate that this cell's surroundings have been checked already.
            CellArray[yInput, xInput].SurroundingMinesChecked = true;

            //Starts one y value above and works its way down
            for (int y = yInput - 1; y <= yInput + 1; y++)
            {
                //Checks y value's bounds
                if (y >= 0 && y < Vertical)
                {
                    //Starts one x value left and works its way right
                    for (int x = xInput - 1; x <= xInput + 1; x++)
                    {
                        //Checks x value's bounds
                        if (x >= 0 && x < Horizontal)
                        {
                            CellArray[y, x].IsSelected = true;

                            //If a revealed cell is a zero, and it has not been checked already...
                            if (CellArray[y, x].SurroundingMinesValue == 0 &&
                                !CellArray[y, x].SurroundingMinesChecked)
                            {
                                //Use recursion to reveal around the newly revealed zeros
                                RevealAroundConnectingZeros(y, x);
                            }
                        }
                    }
                }
            }
        }

        private void GenerateMines(int y, int x)
        {
            BlacklistSurroundingCells(y, x);

            Random rnd = new Random();

            for (int i = 0; i < TotalMines; i++)
            {
                int yRndValue = rnd.Next(Vertical);
                int xRndValue = rnd.Next(Horizontal);

                Cell randomCell = CellArray[yRndValue, xRndValue];

                if (!randomCell.IsMineBlacklisted &&
                    !randomCell.IsMine &&
                    !randomCell.IsSelected)
                {
                    CellArray[yRndValue, xRndValue].IsMine = true;
                }
                else
                {
                    i--;
                }
            }

            GenerateSurroundingMinesValues();
        }

        protected void CreateEmptyCellArray()
        {
            CellArray = new Cell[Vertical, Horizontal];

            //Goes through y values top to bottom
            for (int y = 0; y < Vertical; y++)
            {
                //Goes through x values left to right
                for (int x = 0; x < Horizontal; x++)
                {
                    CellArray[y, x] = new Cell();
                }
            }
        }

        private void BlacklistSurroundingCells(int yInput, int xInput)
        {
            //Starts one y value above and works its way down
            for (int y = yInput - 1; y <= yInput + 1; y++)
            {
                //Checks y value's bounds
                if (y >= 0 && y < Vertical)
                {
                    //Starts one x value left and works its way right
                    for (int x = xInput - 1; x <= xInput + 1; x++)
                    {
                        //Checks x value's bounds
                        if (x >= 0 && x < Horizontal)
                        {
                            CellArray[y, x].IsMineBlacklisted = true;
                        }
                    }
                }
            }
        }

        private void GenerateSurroundingMinesValues()
        {
            //Goes through y values top to bottom
            for (int y = 0; y < Vertical; y++)
            {
                //Goes through x values left to right
                for (int x = 0; x < Horizontal; x++)
                {
                    CellArray[y, x].SurroundingMinesValue = CountSurroundingMines(y, x);
                }
            }
        }

        private int CountSurroundingMines(int yInput, int xInput)
        {
            int surroundingMinesValue = 0;
            //Starts one y value above and works its way down
            for (int y = yInput - 1; y <= yInput + 1; y++)
            {
                //Checks y value's bounds
                if (y >= 0 && y < Vertical)
                {
                    //Starts one x value left and works its way right
                    for (int x = xInput - 1; x <= xInput + 1; x++)
                    {
                        //Checks x value's bounds
                        if (x >= 0 && x < Horizontal)
                        {
                            if (CellArray[y, x].IsMine)
                            {
                                surroundingMinesValue++;
                            }
                        }
                    }
                }
            }
            return surroundingMinesValue;
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

        private void WriteColoredSurroundingMinesValue(int number)
        {
            switch (number)
            {
                case 0:
                    Console.Write(" ");
                    break;

                case 1:
                    Program.PrintColoredString("1", ConsoleColor.Blue);
                    break;

                case 2:
                    Program.PrintColoredString("2", ConsoleColor.Green);
                    break;

                case 3:
                    Program.PrintColoredString("3", ConsoleColor.Red);
                    break;

                case 4:
                    Program.PrintColoredString("4", ConsoleColor.DarkBlue);
                    break;

                case 5:
                    Program.PrintColoredString("5", ConsoleColor.DarkRed);
                    break;

                case 6:
                    Program.PrintColoredString("6", ConsoleColor.DarkCyan);
                    break;

                case 7:
                    Program.PrintColoredString("7", ConsoleColor.Black);
                    break;

                case 8:
                    Program.PrintColoredString("8", ConsoleColor.DarkGray);
                    break;

                default:
                    break;
            }
        }

        public enum GameState
        {
            BlankGameBoard,
            GameInProgress,
            MineSelected,
            GameWon
        }

        public Cell[,] CellArray { get => cellArray; set => cellArray = value; }
        public string Title { get => title; set => title = value; }
        public int Horizontal { get => horizontal; set => horizontal = value; }
        public int Vertical { get => vertical; set => vertical = value; }
        public int TotalMines { get => totalMines; set => totalMines = value; }
        protected bool TwoDigitYAxis { get => twoDigitYAxis; set => twoDigitYAxis = value; }
        protected bool TwoDigitXAxis { get => twoDigitXAxis; set => twoDigitXAxis = value; }
        public GameState State { get => state; set => state = value; }
    }
}

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

        public void CreateBoard(int[,] BoardArray, bool[,] AvailableCells)
        {
            Console.Clear();

            bool TwoDigitXAxis = false; //Initially set  TwoDigitXAxis and TwoDigitYAxis to false.
            bool TwoDigitYAxis = false;

            if (Horizontal > 9) //Sets TwoDigitXAxis to true if Horizontal is greater than 9
                TwoDigitXAxis = true;
            if (Vertical > 9) //Sets TwoDigitYAxis to true if Vertical is greater than 9
                TwoDigitYAxis = true;

            Console.WriteLine(); //Print new line before printing the board

            for (int i = 0; i < Vertical; i++)
            {
                //If there will be 2-digit Y axis, print all the single digits in the Y axis with an extra space at the end so the board is aligned.
                if (TwoDigitYAxis == true && Vertical - i < 10)
                    Console.Write($"{Vertical - i}  "); //Prints Y-axis coordinants with two spaces at the end
                else
                    Console.Write($"{Vertical - i} "); //Prints Y-axis coordinants with one space at the end

                Console.ForegroundColor = ConsoleColor.White;
                for (int j = 0; j < Horizontal; j++)
                {
                    if (AvailableCells[j, Vertical - i - 1] == false)
                        Console.Write("#");
                    else
                        Console.Write(string.Format("{0}", BoardArray[j, Vertical - i - 1]));

                    //If there will be 2-digit X axis, write the board with an extra space after each element to algin the board with the axis.
                    if (TwoDigitXAxis == true)
                        Console.Write("  "); //Print board with two spaces at after each element
                    else
                    Console.Write(" "); //Print board with one space after each element

                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
            }

            //If there will be a 2-digit Y axis, print a three-space left-padding before printing the X-axis.
            if (TwoDigitYAxis == true)
                Console.Write("   "); //3 space left padding before the X-axis coodinants
            else
                Console.Write("  "); //2 space left padding before the x-axis coordinants

            for (int i = 0; i < Horizontal; i++)
            {
                //If there will be 2-digit X-axis, print all the single digit coordinants with an extra space at the end
                if (TwoDigitXAxis == true && i+1 < 10)
                    Console.Write($"{i + 1}  "); //Prints single-digit X-axis coordinants with two spaces at the end
                else
                    Console.Write($"{i + 1} "); //Prints two-digit X-axis coordinants with one space at the end
            }
            Console.Write("\n\n"); //Print two new lines after the board.

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

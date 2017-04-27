using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class Board
    {
        private string title;
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

        public void CreateBoard(int[,] boardArray, bool[,] availableCells)
        {
            bool twoDigitXAxis = false; //Initially set  TwoDigitXAxis and TwoDigitYAxis to false.
            bool twoDigitYAxis = false;

            if (horizontal > 9) //Sets TwoDigitXAxis to true if Horizontal is greater than 9
                twoDigitXAxis = true;
            if (vertical > 9) //Sets TwoDigitYAxis to true if Vertical is greater than 9
                twoDigitYAxis = true;

            Console.WriteLine(); //Print new line before printing the board

            for (int i = 0; i < vertical; i++)
            {
                //If there will be 2-digit Y axis, print all the single digits in the Y axis with an extra space at the end so the board is aligned.
                if (twoDigitYAxis == true && vertical - i < 10)
                    Console.Write($"{vertical - i}  "); //Prints Y-axis coordinants with two spaces at the end
                else
                    Console.Write($"{vertical - i} "); //Prints Y-axis coordinants with one space at the end

                Console.ForegroundColor = ConsoleColor.White;
                for (int j = 0; j < horizontal; j++)
                {
                    if (availableCells[j, Vertical - i - 1] == false)
                        Console.Write("#");
                    else
                        Console.Write(string.Format("{0}", boardArray[j, Vertical - i - 1]));

                    //If there will be 2-digit X axis, write the board with an extra space after each element to algin the board with the axis.
                    if (twoDigitXAxis == true)
                        Console.Write("  "); //Print board with two spaces at after each element
                    else
                        Console.Write(" "); //Print board with one space after each element

                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
            }

            //If there will be a 2-digit Y axis, print a three-space left-padding before printing the X-axis.
            if (twoDigitYAxis == true)
                Console.Write("   "); //3 space left padding before the X-axis coodinants
            else
                Console.Write("  "); //2 space left padding before the x-axis coordinants

            for (int i = 0; i < horizontal; i++)
            {
                //If there will be 2-digit X-axis, print all the single digit coordinants with an extra space at the end
                if (twoDigitXAxis == true && i + 1 < 10)
                    Console.Write($"{i + 1}  "); //Prints single-digit X-axis coordinants with two spaces at the end
                else
                    Console.Write($"{i + 1} "); //Prints two-digit X-axis coordinants with one space at the end
            }
            Console.Write("\n\n"); //Print two new lines after the board.

        }
        public int[,] GenerateMines(int[,] emptyBoardArray)
        {
            Random r = new Random();

            for (int i = 0; i < mines; i++)
            {
                if (emptyBoardArray[r.Next(0, horizontal), r.Next(0, vertical)] == 1)
                    i--;
                else
                    emptyBoardArray[r.Next(0, horizontal), r.Next(0, vertical)] = 1;
            }
            return emptyBoardArray;
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

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }
    }
}

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

        private bool twoDigitXAxis;
        private bool twoDigitYAxis;

        private bool[,] hasMineBoardArray;
        private bool[,] isSelectedBoardArray;
        private bool[,] isFlaggedBoardArray;
        private int[,] surroundingFlagsArray;

        public Board()
        {
            //All board variables will come from EasyBoard.cs, MediumBoard.cs, HardBoard.cs, or CustomerBoard.cs
        }

        public void CreateBoard()
        {
            //Sets TwoDigitXAxis to true if Horizontal is greater than 9
            if (horizontal > 9) 
                twoDigitXAxis = true;

            //Sets TwoDigitYAxis to true if Vertical is greater than 9
            if (vertical > 9) 
                twoDigitYAxis = true;

            //Print new line before printing the board
            Console.WriteLine(); 

            //Outer for loop: rows
            for (int row = 0; row < vertical; row++)
            {
                //Writes the Y Axis coordinants for this row to the console
                WriteYAxisCoodinants(row);

                //Writes the board values for this row
                WriteBoardValues(row);

                //Creates a new line before going on to the next row
                Console.WriteLine();
            }

            WriteXAxisCoordinants();

            //Print two new lines after the board has printed.
            Console.Write("\n\n"); 

        }

        public void WriteYAxisCoodinants(int row)
        {
            //If a 2-digit Y axis exists, print all the single digit axis with an extra space at the end so the axis aligns with the board
            if (twoDigitYAxis == true && vertical - row < 10)
            {
                //Prints Y-axis coordinants < 10 with two spaces at the end (1 digit + 2 spaces = 3 characters total)
                Console.Write($"{vertical - row}  ");
            }
            else
            {
                //Prints Y-axis coordinants > 10 with one space at the end (2 digits + 1 space = 3 characters total)
                Console.Write($"{vertical - row} ");
            }
        }

        public void WriteXAxisCoordinants()
        {
            //If a 2-digit Y exists exists, write three-spaces before writing the X-axis to align with the board.
            if (twoDigitYAxis == true)
            {
                //Write 3 spaces before the X-axis coodinants if 2-digit Y axis values exists
                Console.Write("   ");
            }
            else
            {
                //2 space left padding before the x-axis coordinants
                Console.Write("  ");
            }

            for (int i = 0; i < horizontal; i++)
            {
                //If there will be 2-digit X-axis, print all the single digit coordinants with an extra space at the end
                if (twoDigitXAxis == true && i + 1 < 10)
                {
                    //Prints single-digit X-axis coordinants with two spaces at the end
                    Console.Write($"{i + 1}  ");
                }
                else
                {
                    //Prints two-digit X-axis coordinants with one space at the end
                    Console.Write($"{i + 1} ");
                }
            }
        }

        public void WriteBoardValues(int row)
        {
            //Set console text color to White before printing the board
            Console.ForegroundColor = ConsoleColor.White;

            //Inner for loop: columns
            for (int column = 0; column < horizontal; column++)
            {
                //column  = column index value
                //Vertical - i - 1 = the row index value
                //Checks if the value of the array is "not unavailable" (or available to be selected)
                if (isSelectedBoardArray[column, vertical - row - 1] == false)
                {
                    //Write "#" if the cell is availble to be selected
                    Console.Write("#");
                }
                else
                {
                    if (isFlaggedBoardArray[column, vertical - row - 1] == true)
                        Console.Write("F");
                    else
                    //Write the integer value of boardArray if the cell has been chosen already (or unavailable to be selected)
                    Console.Write(hasMineBoardArray[column, vertical - row - 1].ToString());
                }

                //If there a 2-digit X axis exists, write the board with an extra space after each element to algin the board with the axis.
                if (twoDigitXAxis == true)
                {
                    //Print board with two spaces after each element if 2-digit X axis values exist
                    Console.Write("  ");
                }
                else
                {
                    //Print board with one space after each element if only 1-digit X axis values exist
                    Console.Write(" ");
                }
            }

            //Set console text color back to default (Gray)
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void GenerateMinesBoardArray()
        {
            //Creates new random object
            Random r = new Random();

            int randomHorizontalIndex;
            int randomVerticalIndex;
            bool randomIndexValue;

            bool[,] hasMineBoardArray = new bool[horizontal, vertical];

            //For each mine in the board
            for (int i = 0; i < mines; i++)
            {
                //Generates a random number between 0 and horizontal board size
                randomHorizontalIndex = r.Next(0, horizontal);

                //Generates a random number between 0 and vertical board size
                randomVerticalIndex = r.Next(0, vertical);

                //Finds the value of the a random cell selected by the randomHorizontalIndex and the randomVerticalIndex
                randomIndexValue = hasMineBoardArray[randomHorizontalIndex, randomVerticalIndex];

                //Selects a random index horizontally and vertically.
                //If the selected value is already 1 (mine), decrement i so that the for loop loops an extra time.
                if (randomIndexValue == true)
                    i--;
                else
                {
                    //If the selected value is not 1 (mine), set that value to 1.
                    hasMineBoardArray[randomHorizontalIndex, randomVerticalIndex] = true;
                }
            }
            //set the minesBoardArray for this object to the minesBoardArray created in this method
            this.hasMineBoardArray = hasMineBoardArray;
        }


        //Method to check for surrounding mines and input numbers corresponding to number of surrounding mines for each cell.
        public int CheckForSurroundingMines(int xCoord, int yCoord)

        {
            int nearby_mine_count = 0;


            // Check for mine to the left
            if (hasMineBoardArray[xCoord + 1, yCoord] == true)
                nearby_mine_count++;
            // Check for mine to the right.
            if (hasMineBoardArray[xCoord - 1, yCoord] == true)
                nearby_mine_count++;
            // Check for mine diagonal-dright.
            if (hasMineBoardArray[xCoord + 1, yCoord + 1] == true)
                nearby_mine_count++;
            //Check for mine diagonal-uright.
            if (hasMineBoardArray[xCoord + 1, yCoord - 1] == true)
                nearby_mine_count++;
            //Check for mine diagonal-uleft.
            if (hasMineBoardArray[xCoord - 1, yCoord - 1] == true)
                nearby_mine_count++;
            //Check for mine diagonal-dleft.
            if (hasMineBoardArray[xCoord - 1, yCoord + 1] == true)
                nearby_mine_count++;
            //Check for mine above 
            if (hasMineBoardArray[xCoord, yCoord - 1] == true)
                nearby_mine_count++;
            //Check for mine below
            if (hasMineBoardArray[xCoord, yCoord - 1] == true)
                nearby_mine_count++;

            return nearby_mine_count;
            
        }


            #region properties
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

        public bool[,] MinesBoardArray
        {
            get
            {
                return hasMineBoardArray;
            }

            set
            {
                hasMineBoardArray = value;
            }
        }

        public bool[,] IsSelectedBoardArray
        {
            get
            {
                return isSelectedBoardArray;
            }

            set
            {
                isSelectedBoardArray = value;
            }
        }

        public bool[,] IsFlaggedBoardArray
        {
            get
            {
                return isFlaggedBoardArray;
            }

            set
            {
                isFlaggedBoardArray = value;
            }
        }

        public bool TwoDigitXAxis
        {
            get
            {
                return twoDigitXAxis;
            }

            set
            {
                twoDigitXAxis = value;
            }
        }

        public bool TwoDigitYAxis
        {
            get
            {
                return twoDigitYAxis;
            }

            set
            {
                twoDigitYAxis = value;
            }
        }

        public int[,] SurroundingFlagsArray
        {
            get
            {
                return surroundingFlagsArray;
            }

            set
            {
                surroundingFlagsArray = value;
            }
        }
        #endregion
    }
}

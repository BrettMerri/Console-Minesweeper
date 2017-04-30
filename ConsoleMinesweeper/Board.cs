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

        private int chosenXIndex;
        private int chosenYIndex;

        private bool twoDigitXAxis;
        private bool twoDigitYAxis;

        bool checkAbove = true;
        bool checkBelow = true;
        bool checkLeft = true;
        bool checkRight = true;

        private bool runGame = true;

        private bool[,] hasMineBoardArray;
        private bool[,] isSelectedBoardArray;
        private bool[,] isFlaggedBoardArray;
        private int[,] surroundingMinesArray;

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
                //If WriteBoardValues writes a mine, runGame is set to false and the game will be over
                WriteBoardValues(row);

                //Creates a new line before going on to the next row
                Console.WriteLine();
            }

            WriteXAxisCoordinants();

            //Print two new lines after the board has printed.
            Console.Write("\n\n"); 

        }

        public void RevealAroundAllZeros()
        {
            bool[,] zeroIsNeighbor = new bool[horizontal, vertical];
            bool[,] revealedAroundZeroArray = new bool[horizontal, vertical];

            //Reveals the surrounding cells from the chosen X and Y index.
            RevealSurroundingCells(chosenXIndex, chosenYIndex);
            revealedAroundZeroArray[chosenXIndex, chosenYIndex] = true;
            
            //Outer for loop: rows
            for (int row = 0; row < vertical; row++)
            {
                //Inner for loop: writes columns values
                for (int column = 0; column < horizontal; column++)
                {
                    if (surroundingMinesArray[column, row] == 0 &&
                        revealedAroundZeroArray[column, row] == false)
                    {
                        if (IsANeighbor(column, row, revealedAroundZeroArray))
                        {
                            RevealSurroundingCells(column, row);
                            revealedAroundZeroArray[column, row] = true;
                            row = 0;
                            column = -1;
                        }
                    }
                }
            }
        }

        public bool IsANeighbor(int xIndex, int yIndex, bool[,] revealedAroundZeroArray)
        {
            CheckBothBoundaries(xIndex, yIndex);

            //If we can check above
            if (checkAbove == true)
            {
                //Reveal cell above
                if (revealedAroundZeroArray[xIndex, yIndex - 1])
                    return true;

                //If we can check above and left
                if (checkLeft == true)
                {
                    //Reveal cell up-left
                    if (revealedAroundZeroArray[xIndex - 1, yIndex - 1])
                        return true;
                }
            }

            //If we can check left
            if (checkLeft == true)
            {
                //Reveal cell left
                if (revealedAroundZeroArray[xIndex - 1, yIndex])
                    return true;

                //If we can check left and below
                if (checkBelow == true)
                {
                    //Reveal cell down-left
                    if (revealedAroundZeroArray[xIndex - 1, yIndex + 1])
                        return true;
                }
            }

            //If we can check below
            if (checkBelow == true)
            {
                //Reveal cell below
                if (revealedAroundZeroArray[xIndex, yIndex + 1])
                    return true;

                //If we can check below and right
                if (checkRight == true)
                {
                    //Reveal cell down-right
                    if (revealedAroundZeroArray[xIndex + 1, yIndex + 1])
                        return true;
                }
            }

            //If we can check right
            if (checkRight == true)
            {
                //Reveal cell right
                if (revealedAroundZeroArray[xIndex + 1, yIndex])
                    return true;

                //If we can check right and above
                if (checkAbove == true)
                {
                    //Reveal cell up-right
                    if (revealedAroundZeroArray[xIndex + 1, yIndex - 1])
                        return true;
                }
            }

            //Return false if there are no surrounding zero's.
            return false;
        }

        public void RevealSurroundingCells(int xIndex, int yIndex)
        {
            CheckBothBoundaries(xIndex, yIndex);
            
            //If we can check above
            if (checkAbove == true)
            {
                //Reveal cell above
                isSelectedBoardArray[xIndex, yIndex - 1] = true;

                //If we can check above and left
                if (checkLeft == true)
                {
                    //Reveal cell up-left
                    isSelectedBoardArray[xIndex - 1, yIndex - 1] = true;
                }
            }

            //If we can check left
            if (checkLeft == true)
            {
                //Reveal cell left
                isSelectedBoardArray[xIndex - 1, yIndex] = true;

                //If we can check left and below
                if (checkBelow == true)
                {
                    //Reveal cell down-left
                    isSelectedBoardArray[xIndex - 1, yIndex + 1] = true;
                }
            }

            //If we can check below
            if (checkBelow == true)
            {
                //Reveal cell below
                isSelectedBoardArray[xIndex, yIndex + 1] = true;

                //If we can check below and right
                if (checkRight == true)
                {
                    //Reveal cell down-right
                    isSelectedBoardArray[xIndex + 1, yIndex + 1] = true;
                }

            }

            //If we can check right
            if (checkRight == true)
            {
                //Reveal cell right
                isSelectedBoardArray[xIndex + 1, yIndex] = true;

                //If we can check right and above
                if (checkAbove == true)
                {
                    //Reveal cell up-right
                    isSelectedBoardArray[xIndex + 1, yIndex - 1] = true;
                }
            }
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
            Console.ForegroundColor = ConsoleColor.Black;

            //Inner for loop: writes columns values
            for (int column = 0; column < horizontal; column++)
            {
                //Column index = column 
                //Row index = vertical - i - 1
                int columnIndex = column;
                int rowIndex = vertical - row - 1;

                //Checks if cell is not selected
                if (isSelectedBoardArray[columnIndex, rowIndex] == false)
                {
                    //Write a green "F" if the cell is flagged
                    if (isFlaggedBoardArray[columnIndex, rowIndex] == true)
                    {
                        Program.PrintColoredString("F", "green");
                    }
                    else
                    {
                        //Write "#" if the cell is availble to be selected
                        Console.Write("#");
                    }
                }
                else
                {
                    //If there is no mine, write the number of surrounding mines
                    if (hasMineBoardArray[columnIndex, rowIndex] == false)
                    {
                        int surroundingMinesArrayValue = surroundingMinesArray[columnIndex, rowIndex];

                        //If there are no surrounding mines
                        if (surroundingMinesArrayValue == 0)
                        {
                            //TODO: Write new method here that sets surrounding tile's isSelectedBoardArray value to true;
                            Console.Write(" ");
                        }

                        //If there are surrounding mines (print 1-8 in a certain color)
                        else
                        {
                            string color;
                            if (surroundingMinesArrayValue == 1)
                                color = "blue";
                            else if (surroundingMinesArrayValue == 2)
                                color = "darkgreen";
                            else if (surroundingMinesArrayValue == 3)
                                color = "darkred";
                            else if (surroundingMinesArrayValue == 4)
                                color = "darkblue";
                            else if (surroundingMinesArrayValue == 5)
                                color = "darkred";
                            else if (surroundingMinesArrayValue == 6)
                                color = "darkcyan";
                            else if (surroundingMinesArrayValue == 7)
                                color = "black";
                            else
                                color = "darkgray";

                            Program.PrintColoredString(surroundingMinesArrayValue.ToString(), color);
                        }
                    }
                    //If there is a mine, write a red "X" and set mineSelected to true
                    else
                    {
                        RunGame = false;
                        Program.PrintColoredString("X", "darkred");
                    }
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

            //Set console text color back to default (Black)
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void GenerateMinesBoardArray()
        {
            //Creates new random object
            Random r = new Random();

            int randomHorizontalIndex;
            int randomVerticalIndex;
            bool randomIndexValue;

            bool[,] doNotMakeMinesArea = generateDoNotMakeMinesArea(chosenXIndex, chosenYIndex);

            //For each mine in the board
            for (int i = 0; i < mines; i++)
            {
                //Generates a random number between 0 and horizontal board size
                randomHorizontalIndex = r.Next(0, horizontal);

                //Generates a random number between 0 and vertical board size
                randomVerticalIndex = r.Next(0, vertical);

                //Finds the value of the a random cell selected by the randomHorizontalIndex and the randomVerticalIndex
                randomIndexValue = hasMineBoardArray[randomHorizontalIndex, randomVerticalIndex];

                CheckBothBoundaries(randomHorizontalIndex, randomVerticalIndex);

                //Selects a random index horizontally and vertically.
                //If the selected value is true (mine), decrement i so that the for loop loops an extra time.
                //If the random coordinants = the first selected coordinants, decrement i so that the for loops loops an extra time.
                if (randomIndexValue == true ||
                   doNotMakeMinesArea[randomHorizontalIndex, randomVerticalIndex])
                    i--;
                else
                {
                    //If the selected value is not 1 (mine), set that value to 1.
                    hasMineBoardArray[randomHorizontalIndex, randomVerticalIndex] = true;
                }
            }
        }

        public bool[,] generateDoNotMakeMinesArea(int xIndex, int yIndex)
        {
            bool[,] doNotMakeMinesArea = new bool[horizontal, vertical];

            doNotMakeMinesArea[xIndex, yIndex] = true;

            CheckBothBoundaries(xIndex, yIndex);

            //If we can check above
            if (checkAbove == true)
            {
                //set cell above to true
                doNotMakeMinesArea[xIndex, yIndex - 1] = true;

                //If we can check above and left
                if (checkLeft == true)
                {
                    //Set cell up-left to true
                    doNotMakeMinesArea[xIndex - 1, yIndex - 1] = true;
                }
            }

            //If we can check left
            if (checkLeft == true)
            {
                //Set cell left to true
                doNotMakeMinesArea[xIndex - 1, yIndex] = true;

                //If we can check left and below
                if (checkBelow == true)
                {
                    //Set cell down-left to true 
                    doNotMakeMinesArea[xIndex - 1, yIndex + 1] = true;
                }
            }

            //If we can check below
            if (checkBelow == true)
            {
                //Set cell below to true
                doNotMakeMinesArea[xIndex, yIndex + 1] = true;

                //If we can check below and right
                if (checkRight == true)
                {
                    //Set cell down-right to true
                    doNotMakeMinesArea[xIndex + 1, yIndex + 1] = true;
                }

            }

            //If we can check right
            if (checkRight == true)
            {
                //Set cell right to tryue
                doNotMakeMinesArea[xIndex + 1, yIndex] = true;

                //If we can check right and above
                if (checkAbove == true)
                {
                    //Set cell up-right to true
                    doNotMakeMinesArea[xIndex + 1, yIndex - 1] = true;
                }
            }
            return doNotMakeMinesArea;
        }

        public void CheckXBoundaries(int xIndex)
        {
            int lastXIndex = horizontal - 1;
            checkLeft = true;
            checkRight = true;

            //If the cell is in the first row, do not check the values above
            if (xIndex == 0)
            {
                checkLeft = false;
            }

            //If the cell is in the last row, do not check the values below
            else if (xIndex == lastXIndex)
            {
                checkRight = false;
            }
        }

        public void CheckYBoundaries(int yIndex)
        {
            int lastYIndex = vertical - 1;
            checkBelow = true;
            checkAbove = true;

            //If the cell is in the first row, do not check the values above
            if (yIndex == 0)
            {
                checkAbove = false;
            }

            //If the cell is in the last row, do not check the values below
            else if (yIndex == lastYIndex)
            {
                checkBelow = false;
            }
        }

        public void CheckBothBoundaries(int xIndex, int yIndex)
        {
            CheckXBoundaries(xIndex);
            CheckYBoundaries(yIndex);
        }

        //Uses the minesBoardArray to create the "board behind" containing the numbers of the surrounding mines
        public void CheckForSurroundingMines()
        {
            //Initialize nearbyMineCount to 0
            int nearbyMineCount = 0;

            //Declare the lastRowIndex and lastColumnIndex. These are used so we don't go out of bounds in the array
            int lastRowIndex = vertical - 1;
            int lastColumnIndex = horizontal - 1;

            //Prints top to bottom
            for (int row = 0; row < vertical; row++)
            {
                //row = Y coordinate index
                //Checks the boundries above and below
                CheckYBoundaries(row);

                //If the cell is in the last row, do not check the values below

                //Prints left to right
                for (int column = 0; column < horizontal; column++)
                {

                    //If this cell contains a mine
                    if (hasMineBoardArray[column, row])
                    {
                        //Set this cell's surroundingMinesArray value to -1
                        surroundingMinesArray[column, row] = -1;
                    }
                    else
                    {
                        //column = X coordinate index
                        //Checks the boundaries left and right
                        CheckXBoundaries(column);

                        //If we can check above
                        if (checkAbove == true)
                        {
                            //Check for mine above 
                            if (hasMineBoardArray[column, row - 1] == true)
                                nearbyMineCount++;

                            //If we can check above and to the left
                            if (checkLeft == true)
                            {
                                //Check for mine diagonal-uleft.
                                if (hasMineBoardArray[column - 1, row - 1] == true)
                                    nearbyMineCount++;
                            }
                        }

                        //If we can check left
                        if (checkLeft == true)
                        {
                            // Check for mine to the left
                            if (hasMineBoardArray[column - 1, row] == true)
                                nearbyMineCount++;

                            //If we can check left and below
                            if (checkBelow == true)
                            {
                                //Check for mine diagonal-dleft.
                                if (hasMineBoardArray[column - 1, row + 1] == true)
                                    nearbyMineCount++;
                            }
                        }

                        //If we can check below
                        if (checkBelow == true)
                        {
                            //Check for mine below
                            if (hasMineBoardArray[column, row + 1] == true)
                                nearbyMineCount++;

                            //If we can check below and right
                            if (checkRight == true)
                            {
                                // Check for mine diagonal-dright.
                                if (hasMineBoardArray[column + 1, row + 1] == true)
                                    nearbyMineCount++;
                            }

                        }

                        //If we can check right
                        if (checkRight == true)
                        {
                            // Check for mine to the right.
                            if (hasMineBoardArray[column + 1, row] == true)
                                nearbyMineCount++;

                            //If we can check right and above
                            if (checkAbove == true)
                            {
                                // Check for mine diagonal-uright.
                                if (hasMineBoardArray[column + 1, row - 1] == true)
                                    nearbyMineCount++;
                            }

                        }

                        //Once we check all all of the areas around the cell as we can
                        //Set the value for that index in the surroundingMinesArray to the number of mines that were found
                        surroundingMinesArray[column, row] = nearbyMineCount;

                    }

                    //Reset the nearbyMineCount and checkLeft/checkRight values before checking the next cell in this row
                    nearbyMineCount = 0;
                    checkLeft = true;
                    checkRight = true;
                }

                //Reset the nearbyMineCount and checkAbove/checkBelow values before checking the next cell in this column
                nearbyMineCount = 0;
                checkAbove = true;
                checkBelow = true;
            }
        }


        #region Properties
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

        public bool[,] HasMineBoardArray
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


        public int[,] SurroundingMinesArray
        {
            get
            {
                return surroundingMinesArray;
            }

            set
            {
                surroundingMinesArray = value;
            }
        }

       
        public bool RunGame
        {
            get
            {
                return runGame;
            }

            set
            {
                runGame = value;
            }
        }

        public int ChosenXIndex
        {
            get
            {
                return chosenXIndex;
            }

            set
            {
                chosenXIndex = value;
            }
        }

        public int ChosenYIndex
        {
            get
            {
                return chosenYIndex;
            }

            set
            {
                chosenYIndex = value;
            }
        }

        public bool CheckAbove
        {
            get
            {
                return checkAbove;
            }

            set
            {
                checkAbove = value;
            }
        }

        public bool CheckBelow
        {
            get
            {
                return checkBelow;
            }

            set
            {
                checkBelow = value;
            }
        }

        public bool CheckLeft
        {
            get
            {
                return checkLeft;
            }

            set
            {
                checkLeft = value;
            }
        }

        public bool CheckRight
        {
            get
            {
                return checkRight;
            }

            set
            {
                checkRight = value;
            }
        }
        #endregion
    }
}

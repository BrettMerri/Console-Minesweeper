using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            //Add console title, console colors, console window size, and console window position
            Console.Title = "Console Minesweeper";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowPosition(0, 0);

            Console.WriteLine("Welcome to Console Minesweeper!\n");

            bool run = true;

            while (run)
            {
                //GetGameMode writes a menu and returns a board with the user-selected game mode
                Board currentBoard = GetGameMode();

                //Starts a game with the currentBoard
                StartGame(currentBoard);

                if (!ContinueGame()) //Prompts user if he wants to continue. Set loop to false if use does not want to continue.
                    run = false;
            }
        }

        public static Board GetGameMode()
        {
            //Print Game Mode Menu
            Console.WriteLine("Please select a game mode: ");
            Console.WriteLine("1.) Easy mode");
            Console.WriteLine("2.) Medium mode");
            Console.WriteLine("3.) Hard mode");
            Console.WriteLine("4.) Custom mode");

            //Gets "easy", "medium", or "hard" input from the user
            Console.Write("\nPlease enter a number (1-4): ");
            int menuOptionInput = ConsoleValidation.GetIntegerInRange(1, 4);

            //Easy mode
            if (menuOptionInput == 1)
            {
                //Start game with the easy board
                Board easyBoard = new EasyBoard();
                return easyBoard;
            }

            //Medium mode
            else if (menuOptionInput == 2)
            {
                //Start game with the medium board
                Board mediumBoard = new MediumBoard();
                return mediumBoard;
            }

            //Hard mode
            else if (menuOptionInput == 3)
            {
                //Start game with the hard board
                Board hardBoard = new HardBoard();
                return hardBoard;
            }

            //Custom mode
            else
            {
                //Prompts user for the horizontal size
                Console.Write("Horizontal size (3-30): ");
                int customHorizontalInput = ConsoleValidation.GetIntegerInRange(3, 30);

                //Prompts user for the vertical size
                Console.Write("Vertical size (3-30): ");
                int customVerticalInput = ConsoleValidation.GetIntegerInRange(3, 30);

                int customBoardArea = customHorizontalInput * customVerticalInput;

                //Prompts user for the amount of mines (max mines = custom board area - 1)
                Console.Write($"Amount of mines (1-{customBoardArea - 1}): ");
                int customMinesInput = ConsoleValidation.GetIntegerInRange(1, customBoardArea - 1);

                //Start game with the custom board
                Board customBoard = new CustomBoard(customHorizontalInput, customVerticalInput, customMinesInput);
                return customBoard;
            }
        }

        public static void StartGame(Board currentBoard)
        {
            //Coordinants integers

            int xCoordIndex;
            int yCoordIndex;

            //Board size integers
            int horizontalBoardSize = currentBoard.Horizontal;
            int verticalBoardSize = currentBoard.Vertical;

            //Sets the board's 2-D array, isSelectedBoardArray, to the size of the board. All values start as false.
            currentBoard.IsSelectedBoardArray = new bool[horizontalBoardSize, verticalBoardSize];

            //2D bool Array of flagged cells.  All values in array are initialized to "false".
            currentBoard.IsFlaggedBoardArray = new bool[horizontalBoardSize, verticalBoardSize];

            //Set firstRun to true so the mines will be generated only after the first selection is made.
            bool firstRun = true;

            while (true)
            {
                //Clear console before writing new board
                Console.Clear();

                //Write header showing what game mode and how many mines
                Console.WriteLine($"=== {currentBoard.Title} Mode  ===");
                Console.WriteLine($"=== {currentBoard.Mines} Mines ===");

                //Prints the board
                currentBoard.CreateBoard(); 

                if (currentBoard.RunGame == false)
                {
                    endGame();
                    return;
                }

                //Prompt user for X coordinant
                Console.Write("Enter value for X coordinate: ");

                xCoordIndex = ConsoleValidation.GetIntegerInRange(1, horizontalBoardSize) - 1;

                //Prompt user for Y coordinant
                Console.Write("Enter value for Y coordinate: ");

                yCoordIndex = ConsoleValidation.GetIntegerInRange(1, verticalBoardSize) - 1;

                //Prompt user if he wants to select the flag the coordinate

                Console.WriteLine($"Would you like to [S]elect or [F]lag or [C]ancel coordinate {xCoordIndex + 1}, {yCoordIndex + 1}? (s/f/c): ");

                string selection = ConsoleValidation.GetValidString(new string[] { "s", "f", "c" });

                //If user selectes "s" for Select
                if (selection == "s")
                {
                    //Set the selected coordinant to true in IsSelectedBoardArray
                    //A true IsSelectedBoardArray value makes the cell unavailable
                    currentBoard.IsSelectedBoardArray[xCoordIndex, yCoordIndex] = true;
                }
                
                //If user selected "f" for Flag
                else if (selection == "f")
                {
                    //Set the selected coordinant to true
                    //A true flaggedBoardArray value makes the cell into a flag
                    currentBoard.IsFlaggedBoardArray[xCoordIndex, yCoordIndex] = true;
                    currentBoard.IsSelectedBoardArray[xCoordIndex, yCoordIndex] = true;
                }

                //If user selected "c" for Cancel
                else
                {
                    //Restarts the while loop without changing any values in the array
                    continue;
                }
                if (firstRun == true)
                {
                    //Generates the board's 2-D array, minesBoardArray, with the mines spread out randomly. True = mine. False = no mine.
                    currentBoard.GenerateMinesBoardArray(xCoordIndex, yCoordIndex);
                    currentBoard.CheckForSurroundingMines();

                    //Set firstRun to false afterwards so we only generate the mine locations once.
                    firstRun = false;
                }
            }
        }

        public static void endGame()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You found a mine! Game over.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static bool ContinueGame()
        {
            Console.Write("Do you want to play again? (y/n): "); //Prompt user to type y or n
            string input = ConsoleValidation.GetValidString(new string[] { "y", "n" }); //Gets validated string from the user that is either y or n.
            if (input == "y") //If input is y, write new line and return true
            {
                Console.Clear();
                return true;
            }
            else //If inpus is n, return false
                return false;
        }

    }
}


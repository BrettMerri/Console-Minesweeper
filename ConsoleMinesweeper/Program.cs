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
            //Add default console colors.
            Console.BackgroundColor = ConsoleColor.Black; 
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("Welcome to the Console Minesweeper!\n");

            Board currentBoard = GetGameMode();
            StartGame(currentBoard);
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
                //Prompts user for the horizontal size, vertical size, and amount of mines
                Console.Write("Horizontal size: ");
                int customHorizontalInput = ConsoleValidation.GetIntegerInRange(3, 30);

                Console.Write("Vertical size: ");
                int customVerticalInput = ConsoleValidation.GetIntegerInRange(3, 30);

                int customArea = customHorizontalInput * customVerticalInput;

                Console.Write("Amount of mines: ");
                int customMinesInput = ConsoleValidation.GetIntegerInRange(1, customArea);

                //Start game with the custom board
                Board customBoard = new CustomBoard(customHorizontalInput, customVerticalInput, customMinesInput);
                return customBoard;
            }
        }

        public static void StartGame(Board newBoard)
        {
            int xCoord;
            int yCoord;

            bool[,] availableCells = new bool[newBoard.Horizontal, newBoard.Vertical];
            int[,] emptyBoardArray = new int[newBoard.Horizontal, newBoard.Vertical]; //Create a 2d array with all values as 0's
            int[,] boardArray = newBoard.GenerateMines(emptyBoardArray); //Populates 1's to the array randomly (mines)

            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine($"\n=== {newBoard.Title} Mode - Mines: {newBoard.Mines} ===");
                newBoard.CreateBoard(boardArray, availableCells); //Prints the board

                Console.Write("Enter value for X coordinate: ");
                xCoord = ConsoleValidation.GetIntegerInRange(1, newBoard.Horizontal) - 1;

                Console.Write("Enter value for Y coordinate: ");
                yCoord = ConsoleValidation.GetIntegerInRange(1, newBoard.Vertical) - 1;

                availableCells[xCoord, yCoord] = true;
            }

        }

    }
}

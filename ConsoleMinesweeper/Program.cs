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
            Console.BackgroundColor = ConsoleColor.Black; //Added default console colors.
            Console.ForegroundColor = ConsoleColor.Gray;

            Board easyBoard = new EasyBoard(); //Create all three Board objects
            Board mediumBoard = new MediumBoard();
            Board hardBoard = new HardBoard();

            Console.WriteLine("Welcome to your Minesweeper game application: \n");

            Console.Write("Please select a board type: ");

            //Gets "easy", "medium", or "hard" input from the user
            string selectedBoard = ConsoleValidation.GetValidString(new string[] { "easy", "medium", "hard", "custom" });

            if (selectedBoard == "easy")
                StartGame(easyBoard); //Start game with the easy board
            else if (selectedBoard == "medium")
                StartGame(mediumBoard); //Start game with the medium board
            else if (selectedBoard == "custom")
            {
                Console.Write("Horizontal size: ");
                int customHorizontalInput = ConsoleValidation.GetIntegerInRange(3, 30);

                Console.Write("Vertical size: ");
                int customVerticalInput = ConsoleValidation.GetIntegerInRange(3, 30);

                int customArea = customHorizontalInput * customVerticalInput;

                Console.Write("Amount of mines: ");
                int customMinesInput = ConsoleValidation.GetIntegerInRange(1, customArea);

                Board customBoard = new CustomBoard(customHorizontalInput, customVerticalInput, customMinesInput);
                StartGame(customBoard);
            }
            else
                StartGame(hardBoard); //Start game with the hard board

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

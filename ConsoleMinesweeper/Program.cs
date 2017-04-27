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

            Board easyBoard = new EasyBoard(); //Create all three board objects
            Board mediumBoard = new MediumBoard();
            Board hardBoard = new HardBoard();

            Console.WriteLine("Welcome to your Minesweeper game application: \n");
            Console.Write("Please select a board type: ");

            //Gets a string input from the user that is validated to only be "easy", "medium", or "hard"
            string SelectedBoard = ConsoleValidation.GetValidString(new string[] { "easy", "medium", "hard" });

            Console.WriteLine($"You selected the {SelectedBoard} board.");

            
            if (SelectedBoard == "easy")
            {
                StartGame(easyBoard); //Start game with the easy board
            }
            else if (SelectedBoard == "medium") 
            {
                StartGame(mediumBoard); //Start game with the medium board
            }
            else
            {
                StartGame(hardBoard); //Start game with the hard board
            }

            
            //Console.WriteLine("Easy board:");
            //Board board1 = new EasyBoard();
            //board1.CreateBoard();

            //Console.WriteLine("Medium board:");
            //Board board2 = new MediumBoard();
            //board2.CreateBoard();

            //Console.WriteLine("Hard board:");
            //Board board3 = new HardBoard();
            //board3.CreateBoard();


        }

        public static void StartGame(Board newBoard)
        {
            int xCoord;
            int yCoord;

            bool[,] AvailableCells = new bool[newBoard.Horizontal, newBoard.Vertical];

            int[,] EmptyBoardArray = new int[newBoard.Horizontal, newBoard.Vertical]; //Create a 2d array with all values as 0's
            int[,] BoardArray = newBoard.GenerateMines(EmptyBoardArray); //Populates 1's to the array randomly (mines)

            newBoard.CreateBoard(BoardArray, AvailableCells); //Prints the board

            bool run = true;
            while (run)
            {

                Console.Write("Enter value for X coordinate: ");
                xCoord = int.Parse(Console.ReadLine()) - 1;

                Console.Write("Enter value for Y coordinate: ");
                yCoord = int.Parse(Console.ReadLine()) - 1;

                AvailableCells[xCoord, yCoord] = true;

                newBoard.CreateBoard(BoardArray, AvailableCells);
            }

        }

    }
}

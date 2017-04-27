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
            newBoard.CreateBoard(); //Prints the board with coordinants
            Console.Write("Enter your co-ordinants: ");
        }

    }
}

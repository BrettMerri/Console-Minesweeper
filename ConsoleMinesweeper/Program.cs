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

            Board easyBoard = new EasyBoard();
            Board mediumBoard = new MediumBoard();
            Board hardBoard = new HardBoard();

            Console.WriteLine("Welcome to your Minesweeper game application: \n");
            Console.Write("Please select a board type: ");
            string SelectedBoard = ConsoleValidation.GetValidString(new string[] { "easy", "medium", "hard" });

            Console.WriteLine($"You selected the {SelectedBoard} board.");

            
            if (SelectedBoard == "easy")
            {
                StartGame(easyBoard);

            }
            else if (SelectedBoard == "medium") 
            {
                StartGame(mediumBoard);
            }
            else
            {
                StartGame(hardBoard);
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
            newBoard.CreateBoard();
            Console.Write("Enter your co-ordinants: ");
        }

    }
}

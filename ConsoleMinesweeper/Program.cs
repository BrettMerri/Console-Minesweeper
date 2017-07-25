using System;
using System.Media;
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
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowPosition(0, 0);

            Console.WriteLine("Welcome to Console Minesweeper!\n");

            bool run = true;

            while (run)
            {
                //GetGameMode writes a menu and returns a board with the user-selected game mode
                Board currentBoard = GetGameMode();

                //Starts a game with the currentBoard
                PlayGame(currentBoard);

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

            //Gets 1, 2, 3, or 4 input from the user
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
            else //if (menuOptionInput == 4)
            {
                //Prompts user for the horizontal size
                Console.Write("Horizontal size (5-30): ");
                int customHorizontalInput = ConsoleValidation.GetIntegerInRange(5, 30);

                //Prompts user for the vertical size
                Console.Write("Vertical size (5-30): ");
                int customVerticalInput = ConsoleValidation.GetIntegerInRange(5, 30);

                int customBoardArea = customHorizontalInput * customVerticalInput;

                //Prompts user for the amount of mines (max mines = customBoardArea - 9)
                //customBoardArea - 9 is so we can ensure that the user's first click is a 0.
                Console.Write($"Amount of mines (1-{customBoardArea - 9}): ");
                int customMinesInput = ConsoleValidation.GetIntegerInRange(1, customBoardArea - 9);

                //Start game with the custom board
                Board customBoard = new CustomBoard(customHorizontalInput, customVerticalInput, customMinesInput);
                return customBoard;
            }
        }

        public static void PlayGame(Board currentBoard)
        {
            bool runGame = true;

            while (runGame)
            {
                Console.Clear();

                Console.WriteLine($"{currentBoard.Title} Mode - {currentBoard.TotalMines} mines");

                currentBoard.WriteBoard();

                if (currentBoard.State == Board.GameState.MineSelected)
                {
                    (new SoundPlayer("../../audio/Lose.wav")).Play();
                    Console.WriteLine("You hit a mine! Game over.");
                    return;
                }
                else if (currentBoard.State == Board.GameState.GameWon)
                {
                    (new SoundPlayer("../../audio/Win.wav")).Play();
                    Console.WriteLine("You win!");
                    return;
                }

                Console.WriteLine("\nTo play you must type a coordinate followed by S or F to select or flag.");
                Console.WriteLine("For example: '4/5 S' to select 4/5 or '2/1 F' to flag 2/1.\n");
                InputCoordinates coordinates = ConsoleValidation.GetValidCoordinates(currentBoard.Vertical, currentBoard.Horizontal);

                //Convert typed coordinates to coordinate indexes for the array of the cells
                int yCoord = currentBoard.Vertical - coordinates.Y;
                int xCoord = coordinates.X - 1;

                if (coordinates.Option == SelectOrFlag.S)
                {
                    (new SoundPlayer("../../audio/Clear.wav")).Play();
                    currentBoard.SelectCell(yCoord, xCoord);
                }
                else if (coordinates.Option == SelectOrFlag.F)
                {
                    currentBoard.FlagCell(yCoord, xCoord);
                }
            }
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

        public static void PrintColoredString(string stringValue, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            
            Console.Write(stringValue);

            //After writing the colored string, change color back to black.
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
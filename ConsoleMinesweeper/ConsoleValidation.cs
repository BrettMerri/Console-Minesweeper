using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleMinesweeper
{
    class ConsoleValidation
    {

        public static string GetValidString(string[] options)
        {
            string input;
            string listOfOptions = string.Join(", ", options); //Creates a string of all elements in the Options array seperated by a comma.

            while (true)
            {
                input = Console.ReadLine().Trim().ToLower(); //Gets a lowercase input from user.

                foreach (string item in options) //Checks if user input equals any string in the Options array.
                {
                    if (input == item) //If user input equals an option, return the input.
                        return input;
                }
                //If user input does not equal any of the options, write a list of options to choose from and have them try again.
                Program.PrintColoredString($"Input must be either of the following: [{listOfOptions}]. Try again: ", ConsoleColor.DarkRed);
            }
        }

        public static int GetValidInteger()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input)) //While user input is unable to be parsed into an integer, display error.
            {
                Program.PrintColoredString("Invalid input. Try again: ", ConsoleColor.DarkRed);
            }
            return input;
        }

        public static int GetIntegerInRange(int min, int max)
        {
            int input = GetValidInteger(); //Gets a valid integer from user input
            while (input < min || input > max) //While input is less than the min or greater than the max, display an error.
            {
                Program.PrintColoredString($"Input not between {min} and {max}. Try again: ", ConsoleColor.DarkRed);
                input = GetValidInteger(); //Get another valid integer from the user if input is not in range.
            }
            return input;
        }

        public static InputCoordinates GetValidCoordinates(int vertical, int horizontal)
        {
            while (true)
            {
                string input = Console.ReadLine().Trim().ToLower();
                string[] values;

                //Regex validates input is a number between 0 - 30, slash, number between 0 - 30, space, s or f character
                if (!Regex.IsMatch(input, @"([0-9]|[1-2][0-9]|30)\/([0-9]|[1-2][0-9]|30)\s[sf]"))
                {
                    Program.PrintColoredString("Invalid input. Try again: ", ConsoleColor.DarkRed);
                    continue;
                }

                values = input.Split(new char[] { '/', ' ' });

                if (!int.TryParse(values[0], out int x))
                {
                    Program.PrintColoredString("Invalid X input. Try again: ", ConsoleColor.DarkRed);
                    continue;
                }
                    
                if (!int.TryParse(values[1], out int y))
                {
                    Program.PrintColoredString("Invalid Y input. Try again: ", ConsoleColor.DarkRed);
                    continue;
                }

                if (x < 1 || x > horizontal)
                {
                    Program.PrintColoredString($"X input is not between 1 and {horizontal}. Try again: ", ConsoleColor.DarkRed);
                    continue;
                }

                if (y < 1 || y > vertical)
                {
                    Program.PrintColoredString($"Y input is not between 1 and {vertical}. Try again: ", ConsoleColor.DarkRed);
                    continue;
                }

                if (!Enum.TryParse(values[2].ToUpper(), out SelectOrFlag option))
                {
                    Program.PrintColoredString($"Action is not S or F. Try again: ", ConsoleColor.DarkRed);
                    continue;
                }

                return new InputCoordinates(y, x, option);
            }
        }

    }
}

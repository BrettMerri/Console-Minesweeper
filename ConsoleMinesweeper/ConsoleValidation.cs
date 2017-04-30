using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    class ConsoleValidation
    {

        public static string GetValidString(string[] Options)
        {
            string Input;
            string ListOfOptions = string.Join(", ", Options); //Creates a string of all elements in the Options array seperated by a comma.

            while (true)
            {
                Input = Console.ReadLine().ToLower(); //Gets a lowercase input from user.

                foreach (string item in Options) //Checks if user input equals any string in the Options array.
                {
                    if (Input == item) //If user input equals an option, return the input.
                        return Input;
                }
                //If user input does not equal any of the options, write a list of options to choose from and have them try again.
                Program.PrintColoredString($"Input must be either of the following: [{ListOfOptions}]. Try again: ", "darkred");
            }
        }

        public static int GetValidInteger()
        {
            int Input;
            while (!int.TryParse(Console.ReadLine(), out Input)) //While user input is unable to be parsed into an integer, display error.
            {
                Program.PrintColoredString("Invalid input. Try again: ", "darkred");
            }
            return Input;
        }

        public static int GetIntegerInRange(int Min, int Max)
        {
            int Input = GetValidInteger(); //Gets a valid integer from user input
            while (Input < Min || Input > Max) //While input is less than the min or greater than the max, display an error.
            {
                Program.PrintColoredString($"Input not between {Min} and {Max}. Try again: ", "darkred");
                Input = GetValidInteger(); //Get another valid integer from the user if input is not in range.
            }
            return Input;
        }

    }
}

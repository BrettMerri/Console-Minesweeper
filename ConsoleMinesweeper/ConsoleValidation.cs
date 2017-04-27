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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Input must be either of the following: [{ListOfOptions}]. Try again: ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }
}

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

            Console.WriteLine("Easy board:");
            EasyBoard board1 = new EasyBoard();
            board1.CreateBoard();

            Console.WriteLine("Medium board:");
            MediumBoard board2 = new MediumBoard();
            board2.CreateBoard();

            Console.WriteLine("Hard board:");
            HardBoard board3 = new HardBoard();
            board3.CreateBoard();


        }

    }
}

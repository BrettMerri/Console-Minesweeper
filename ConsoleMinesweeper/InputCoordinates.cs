using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    public class InputCoordinates
    {
        private int x;
        private int y;
        private SelectOrFlag option;

        public InputCoordinates(int x, int y, SelectOrFlag option)
        {
            this.x = x;
            this.y = y;
            this.option = option;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public SelectOrFlag Option { get => option; set => option = value; }
    }
    public enum SelectOrFlag
    {
        S,
        F
    };
}

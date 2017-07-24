using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    public class InputCoordinates
    {
        private int y;
        private int x;
        private SelectOrFlag option;

        public InputCoordinates(int y, int x, SelectOrFlag option)
        {
            this.y = y;
            this.x = x;
            this.option = option;
        }

        public int Y { get => y; set => y = value; }
        public int X { get => x; set => x = value; }
        public SelectOrFlag Option { get => option; set => option = value; }
    }
    public enum SelectOrFlag
    {
        S,
        F
    };
}

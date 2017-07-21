using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMinesweeper
{
    public class Cell
    {
        private bool isMine;
        private bool isSelected;
        private bool isFlagged;
        private bool isMineBlacklisted; //MineBlacklisted means a mine cannot be generated at this cell

        private bool checkAbove;
        private bool checkBelow;
        private bool checkLeft;
        private bool checkRight;

        private int xCoord;
        private int yCoord;
        private int surroundingMinesValue;


        public Cell(int y, int x, int yMax, int xMax)
        {
            SurroundingMinesValue = 0;

            isMine = false;
            isSelected = false;
            isFlagged = false;

            CheckAbove = true;
            CheckBelow = true;
            CheckLeft = true;
            CheckRight = true;

            if (y == 0)
            {
                CheckAbove = false;
            }
            else if (y == yMax)
            {
                CheckBelow = false;
            }

            if (x == 0)
            {
                CheckLeft = false;
            }
            else if (x == xMax)
            {
                CheckRight = false;
            }
        }

        public bool CheckAbove { get; private set; }
        public bool CheckBelow { get; private set; }
        public bool CheckLeft { get; private set; }
        public bool CheckRight { get; private set; }
        public int SurroundingMinesValue { get; private set; }

        public bool IsMine { get; set; }
        public bool IsSelected { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsMineBlacklisted { get; set; }
    }
}

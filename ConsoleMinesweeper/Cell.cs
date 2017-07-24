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

        private int surroundingMinesValue;


        public Cell()
        {
            SurroundingMinesValue = 0;

            isMine = false;
            isSelected = false;
            isFlagged = false;
        }

        public bool IsMine { get => isMine; set => isMine = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public bool IsFlagged { get => isFlagged; set => isFlagged = value; }
        public bool IsMineBlacklisted { get => isMineBlacklisted; set => isMineBlacklisted = value; }
        public int SurroundingMinesValue { get => surroundingMinesValue; private set => surroundingMinesValue = value; }
    }
}

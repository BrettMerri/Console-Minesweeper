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
        private bool isMineBlacklisted; //isMineBlacklisted means a mine cannot be generated at this cell
        private bool surroundingMinesChecked; //isConnected is used to check if surrounding cells have already been checked when revealing all connecting zeros

        private int surroundingMinesValue;


        public Cell()
        {
            SurroundingMinesValue = 0;

            isMine = false;
            isSelected = false;
            isFlagged = false;
            isMineBlacklisted = false;
            surroundingMinesChecked = false;
        }

        public bool IsMine { get => isMine; set => isMine = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public bool IsFlagged { get => isFlagged; set => isFlagged = value; }
        public bool IsMineBlacklisted { get => isMineBlacklisted; set => isMineBlacklisted = value; }
        public int SurroundingMinesValue { get => surroundingMinesValue; set => surroundingMinesValue = value; }
        public bool SurroundingMinesChecked { get => surroundingMinesChecked; set => surroundingMinesChecked = value; }
    }
}

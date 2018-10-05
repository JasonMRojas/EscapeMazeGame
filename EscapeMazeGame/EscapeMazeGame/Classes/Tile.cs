using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMazeGame.Classes
{
    public class Tile
    {
        public string Display
        {
            get
            {
                if (Value == 1)
                {
                    return "S";
                }
                else if (Value == 2)
                {
                    return "E";
                }
                else
                {
                    return " ";
                }
            }
        }

        public int Value
        {
            get
            {
                if (IsStartTile)
                {
                    return 1;
                }
                else if (IsEndTile)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool IsStartTile { get; set; }

        public bool IsEndTile { get; set; }

        public Tile(bool isStart, bool isEnd)
        {
            this.IsStartTile = isStart;
            this.IsEndTile = isEnd;
        }
    }
}

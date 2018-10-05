using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMazeGame.Classes
{
    public class Wall
    {
        public string Display { get; }

        public int Value { get; }

        public Wall()
        {
            Display = "X";
            Value = -1;
        }
    }
}

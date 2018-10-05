using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMazeGame.Classes
{
    interface ICharacter
    {
        int[] Position { get; set; }

        void Move(Map map);


    }
}

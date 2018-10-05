using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMazeGame.Classes
{
    public class Drone : ICharacter
    {
        public int[] Position { get; set; }

        public virtual int Value { get; }

        public Drone()
        {
            Position = new int[] { 1, 1 };
        }

        public virtual void Move(Map map)
        {
            Random random = new Random();
            Wall wall = new Wall();
            int nextDirection = random.Next(1, 9);

            switch (nextDirection)
            {
                case 1:

                    if (Position[0] + 1 < map.MapArrayOfArrays.Length - 1)
                    {
                        Position[0]++;
                    }
                    break;
                case 2:

                    if (Position[0] - 1 > 0)
                    {
                        Position[0]--;
                    }
                    break;
                case 7:
                case 5:
                case 3:
                    if (Position[1] - 1 > 0)
                    {
                        Position[1]--;
                    }
                    break;
                case 8:
                case 6:
                case 4:
                    if (Position[1] + 1 < map.MapArrayOfArrays[0].Length - 1)
                    {
                        Position[1]++;
                    }
                    break;

            }
        }
    }
}

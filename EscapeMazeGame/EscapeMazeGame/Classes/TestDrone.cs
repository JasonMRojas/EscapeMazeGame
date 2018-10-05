using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMazeGame.Classes
{
    public class TestDrone : Drone
    {
        public override void Move(Map testMap)
        {
            Random random = new Random();
            Wall wall = new Wall();
            int nextDirection = random.Next(1, 5);
            switch (nextDirection)
            {
                case 1:
                    if (testMap.MapArrayOfArrays[Position[0] + 1][Position[1]] != wall.Value)
                    {
                        Position[0]++;
                    }
                    break;
                case 2:
                    if (testMap.MapArrayOfArrays[Position[0] - 1][Position[1]] != wall.Value)
                    {
                        Position[0]--;
                    }
                    break;
                case 3:
                    if (testMap.MapArrayOfArrays[Position[0]][Position[1] - 1] != wall.Value)
                    {
                        Position[1]--;
                    }
                    break;
                case 4:
                    if (testMap.MapArrayOfArrays[Position[0]][Position[1] + 1] != wall.Value)
                    {
                        Position[1]++;
                    }
                    break;

            }
        }
    }
}

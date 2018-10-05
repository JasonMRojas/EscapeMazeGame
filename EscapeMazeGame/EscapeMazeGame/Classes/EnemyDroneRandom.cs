using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMazeGame.Classes
{
    public class EnemyDroneRandom : Drone
    {
        public override int Value { get; }

        public string Display { get; }

        public EnemyDroneRandom(Map map)
        {
            this.Position = this.StartingDronePostion(map);
            this.Value = -3;
            this.Display = "D";
        }

        public override void Move(Map map)
        {
            Random random = new Random();
            Wall wall = new Wall();
            int[] pDronePos = new int[2];
            pDronePos[0] = this.Position[0];
            pDronePos[1] = this.Position[1];
            bool notStuck = false;
            int howLongStuck = 0;
            do
            {
                int nextDirection = random.Next(1, 5);
                switch (nextDirection)
                {
                    case 1:
                        if (map.MapArrayOfArrays[Position[0] + 1][Position[1]] != wall.Value
                            && map.MapArrayOfArrays[Position[0] + 1][Position[1]] != 2)
                        {
                            Position[0]++;
                            notStuck = true;
                        }
                        else
                        {
                            notStuck = false;
                        }
                        break;
                    case 2:
                        if (map.MapArrayOfArrays[Position[0] - 1][Position[1]] != wall.Value
                            && map.MapArrayOfArrays[Position[0] - 1][Position[1]] != 2)
                        {
                            Position[0]--;
                            notStuck = true;
                        }
                        else
                        {
                            notStuck = false;
                        }
                        break;
                    case 3:
                        if (map.MapArrayOfArrays[Position[0]][Position[1] - 1] != wall.Value
                            && map.MapArrayOfArrays[Position[0]][Position[1] - 1] != 2)
                        {
                            Position[1]--;
                            notStuck = true;
                        }
                        else
                        {
                            notStuck = false;
                        }
                        break;
                    case 4:
                        if (map.MapArrayOfArrays[Position[0]][Position[1] + 1] != wall.Value
                            && map.MapArrayOfArrays[Position[0]][Position[1] + 1] != 2)
                        {
                            Position[1]++;
                            notStuck = true;
                        }
                        else
                        {
                            notStuck = false;
                        }
                        break;
                }

                howLongStuck++;

            } while (!notStuck && howLongStuck < 20);
            if (this.Position[0] != pDronePos[0] || this.Position[1] != pDronePos[1])
            {
                map.UpdateDronePostion(pDronePos, this);
            }
        }

        public int[] StartingDronePostion(Map map)
        {
            Random random = new Random();
            int randomSeedI = random.Next(2, map.MapArrayOfArrays.Length - 2);
            int randomSeedJ = random.Next(2, map.MapArrayOfArrays[map.MapArrayOfArrays.Length - 2].Length - 2);
            int[] returnedInt = new int[2];
            for (int i = randomSeedI; i < map.MapArrayOfArrays.Length; i++)
            {
                for (int j = randomSeedJ; j < map.MapArrayOfArrays[i].Length; j++)
                {
                    int[] currentPosition = new int[2];
                    currentPosition[0] = i;
                    currentPosition[1] = j;
                    if (map.MapArrayOfArrays[i][j] == 0 && !map.IsSurrounded(map.MapArrayOfArrays, currentPosition, -1))
                    {
                        map.MapArrayOfArrays[i][j] = -3;
                        returnedInt[0] = i;
                        returnedInt[1] = j;
                        break;
                    }
                }
                if (i == returnedInt[0])
                {
                    break;
                }
            }
            return returnedInt;
        }
    }
}
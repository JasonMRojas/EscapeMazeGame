using System;
using System.Collections.Generic;
using System.Text;
using EscapeMazeGame.Classes;

namespace EscapeMazeGame.Classes
{
    public class EnemyDroneDiggerAggressive : Drone
    {
        public override int Value { get; }

        public string Display { get; }

        private int MoveCounter { get; set; }

        public EnemyDroneDiggerAggressive(Map map)
        {
            this.Position = this.StartingDronePostion(map);
            this.Value = -5;
            this.Display = "H";
            this.MoveCounter = 0;
        }

        public override void Move(Map map)
        {
            int[] pDronePos = new int[2];
            pDronePos[0] = this.Position[0];
            pDronePos[1] = this.Position[1];
            int[] playerPos = new int[2];
            this.MoveCounter++;
            if (this.MoveCounter <= 2)
            {
                base.Move(map); //move randomly
            }
            else
            {
                for (int i = 0; i < map.MapArrayOfArrays.Length; i++) //Find player position
                {
                    for (int j = 0; j < map.MapArrayOfArrays[i].Length; j++)
                    {
                        if (map.MapArrayOfArrays[i][j] == -2)
                        {
                            playerPos[0] = i;
                            playerPos[1] = j;
                        }
                    }
                }

                int distancePlayerDroneX = playerPos[1] - this.Position[1];
                int distancePlayerDroneY = playerPos[0] - this.Position[0];

                if (Math.Abs(distancePlayerDroneX) > Math.Abs(distancePlayerDroneY))
                {
                    if (distancePlayerDroneX > 0)
                    {
                        this.Position[1]++;
                    }
                    else
                    {
                        this.Position[1]--;
                    }
                }
                else
                {
                    if (distancePlayerDroneY > 0)
                    {
                        this.Position[0]++;
                    }
                    else
                    {
                        this.Position[0]--;
                    }
                }


            }

            if (this.MoveCounter > 10)
            {
                this.MoveCounter = 0;
            }

            map.UpdateDronePostion(pDronePos, this);

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
                        map.MapArrayOfArrays[i][j] = -5;
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

using System;
using System.Collections.Generic;
using System.Text;
using EscapeMazeGame.Classes;

namespace EscapeMazeGame.Classes
{
    public class PlayerCharacter : ICharacter
    {
        public string Name { get; }
        public int[] Position { get; set; }

        public string Display { get { return "P"; } }

        public int Value { get; }

        public PlayerCharacter(string name)
        {
            this.Name = name;

            this.Position = new int[2];
            this.Position[0] = 1; //Position of i
            this.Position[1] = 1; //Position of j
            this.Value = -2;
        }

        public void Move(Map currentMap)
        {
            ConsoleKeyInfo input = new ConsoleKeyInfo();
            input = Console.ReadKey();
            Wall wall = new Wall();
            int pX = this.Position[1];
            int pY = this.Position[0];
            int[] pCharacterPos = new int[2];
            pCharacterPos[0] = pY;
            pCharacterPos[1] = pX;

            switch (input.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (currentMap.MapArrayOfArrays[Position[0] - 1][Position[1]] != wall.Value)
                    {
                        this.Position[0]--;
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (currentMap.MapArrayOfArrays[Position[0]][Position[1] + 1] != wall.Value)
                    {
                        this.Position[1]++;
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (currentMap.MapArrayOfArrays[Position[0] + 1][Position[1]] != wall.Value)
                    {
                        this.Position[0]++;
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (currentMap.MapArrayOfArrays[Position[0]][Position[1] - 1] != wall.Value)
                    {
                        this.Position[1]--;
                    }
                    break;
            }

            if (this.Position[0] != pCharacterPos[0] || this.Position[1] != pCharacterPos[1])
            {
                currentMap.UpdateCharacterPostion(pCharacterPos, this);
            }

        }
    }
}

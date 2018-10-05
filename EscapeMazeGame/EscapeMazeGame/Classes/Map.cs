using System;
using System.Collections.Generic;
using System.Text;
using EscapeMazeGame.Classes;

namespace EscapeMazeGame.Classes
{
    public class Map
    {
        /// <summary>
        /// Holds the display in the form of a string
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// Holds the values in the map in an Array of Arrays. Its is basically a grid
        /// </summary>
        public int[][] MapArrayOfArrays { get; private set; }

        /// <summary>
        /// Holds the height of the grid
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Holds the width of the grid
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Builds the map based off of the character and the height and width
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="character"></param>
        public Map(int width, int height, PlayerCharacter character)
        {
            this.Height = height;
            this.Width = width;
            this.MapArrayOfArrays = BuildMapArrayEntirely(character);
            this.MapArrayOfArrays[1][1] = character.Value;
        }

        /// <summary>
        /// Updates the positions of the various drones on the map values
        /// </summary>
        /// <param name="pDronePos"></param>
        /// <param name="newDronePosition"></param>
        public void UpdateDronePostion(int[] pDronePos, Drone newDronePosition)
        {
            Tile previousTile = new Tile(false, false);
            this.MapArrayOfArrays[newDronePosition.Position[0]][newDronePosition.Position[1]] = newDronePosition.Value;
            this.MapArrayOfArrays[pDronePos[0]][pDronePos[1]] = previousTile.Value;
        }

        /// <summary>
        /// Updates the characters position.
        /// </summary>
        /// <param name="pCharacterPos"></param>
        /// <param name="newCharacterPosition"></param>
        public void UpdateCharacterPostion(int[] pCharacterPos, PlayerCharacter newCharacterPosition)
        {
            Tile previousTile = new Tile(false, false);
            if (this.MapArrayOfArrays[newCharacterPosition.Position[0]][newCharacterPosition.Position[1]] != -3
                && this.MapArrayOfArrays[newCharacterPosition.Position[0]][newCharacterPosition.Position[1]] != -4
                && this.MapArrayOfArrays[newCharacterPosition.Position[0]][newCharacterPosition.Position[1]] != -5)
            {
                this.MapArrayOfArrays[newCharacterPosition.Position[0]][newCharacterPosition.Position[1]] = newCharacterPosition.Value;
                this.MapArrayOfArrays[pCharacterPos[0]][pCharacterPos[1]] = previousTile.Value;
            }
            else
            {
                this.MapArrayOfArrays[pCharacterPos[0]][pCharacterPos[1]] = previousTile.Value;
            }

        }

        /// <summary>
        /// Runs the various methods to build the map array
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private int[][] BuildMapArrayEntirely(PlayerCharacter character)
        {
            int[][] finishedMapArray = new int[this.Height][];
            do
            {
                BuildMapGrid(character);
                BuildMazeInGrid();
            } while (!IsValidMaze());

            return this.MapArrayOfArrays;
        }

        /// <summary>
        /// Carves the maze into the grid. Uses a robot to randomly do it
        /// </summary>
        private void BuildMazeInGrid()
        {
            ConstructorDrone constructor = new ConstructorDrone();
            Random randomSeed = new Random();
            for (int i = 0; i < this.MapArrayOfArrays.Length; i++)
            {
                for (int j = 0; j < this.MapArrayOfArrays[i].Length * 4; j++)
                {
                    int randomSeedNumber = randomSeed.Next(0, 3);
                    constructor.Move(this);
                    if (this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] != 2)
                    {
                        if (!IsSurrounded(this.MapArrayOfArrays, constructor, 0))
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = 0;
                        }
                        else
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = -1;
                        }
                        if (this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] == -1 && IsSurrounded(this.MapArrayOfArrays, constructor, 0) && randomSeedNumber < 2)
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = 0;
                        }
                        if (IsNextTo(this.MapArrayOfArrays, constructor, 2))
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = 0;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a drone is surrounded by a tile value
        /// </summary>
        /// <param name="mapMazeArray"></param>
        /// <param name="constructor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsSurrounded(int[][] mapMazeArray, ConstructorDrone constructor, int value)
        {
            return mapMazeArray[constructor.Position[0] + 1][constructor.Position[1]] == value
                                && mapMazeArray[constructor.Position[0]][constructor.Position[1] + 1] == value
                                && mapMazeArray[constructor.Position[0]][constructor.Position[1] - 1] == value
                                && mapMazeArray[constructor.Position[0] - 1][constructor.Position[1]] == value;
        }

        /// <summary>
        /// Checks if a drone is next to a tile value
        /// </summary>
        /// <param name="mapMazeArray"></param>
        /// <param name="constructor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsNextTo(int[][] mapMazeArray, ConstructorDrone constructor, int value)
        {
            return mapMazeArray[constructor.Position[0] + 1][constructor.Position[1]] == value
                                || mapMazeArray[constructor.Position[0]][constructor.Position[1] + 1] == value
                                || mapMazeArray[constructor.Position[0]][constructor.Position[1] - 1] == value
                                || mapMazeArray[constructor.Position[0] - 1][constructor.Position[1]] == value;
        }

        /// <summary>
        /// Checks if a generic position is surrounded by a value
        /// </summary>
        /// <param name="mapMazeArray"></param>
        /// <param name="position"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsSurrounded(int[][] mapMazeArray, int[] position, int value)
        {
            return mapMazeArray[position[0] + 1][position[1]] == value
                                && mapMazeArray[position[0]][position[1] + 1] == value
                                && mapMazeArray[position[0]][position[1] - 1] == value
                                && mapMazeArray[position[0] - 1][position[1]] == value;
        }

        /// <summary>
        /// Checks to see if the current maze combination is valid, IE the start can get to the end.
        /// </summary>
        /// <returns></returns>
        private bool IsValidMaze()
        {
            // Loop over rows and then columns.
            bool isValid = false;
            Tile currentTile = new Tile(false, false);
            TestDrone tester = new TestDrone();
            int moveCounter = 0;
            double xCounter = 0;
            for (int i = 0; i < this.MapArrayOfArrays.Length - 1; i++)
            {
                for (int j = 0; j < this.MapArrayOfArrays[i].Length - 1; j++)
                {
                    if (this.MapArrayOfArrays[i][j] == -1 && i != 0 && j != 0)
                    {
                        xCounter++;
                    }
                }
            }
            while (!currentTile.IsEndTile && moveCounter < (this.MapArrayOfArrays.Length * 5 * this.MapArrayOfArrays[0].Length * 5))
            {
                tester.Move(this);
                if (this.MapArrayOfArrays[tester.Position[0]][tester.Position[1]] == 2)
                {
                    currentTile.IsEndTile = true;
                    isValid = true;
                }
                moveCounter++;
            }
            return isValid;
        }

        /// <summary>
        /// Constructs a grid of walls at the size designated by the height and width
        /// </summary>
        /// <param name="character"></param>
        private void BuildMapGrid(PlayerCharacter character)
        {
            Wall wall = new Wall();
            int[][] mapArray = new int[this.Height][];
            for (int i = 0; i < mapArray.Length; i++)
            {
                //int[] row = new int[mapArray.Length]; Not yet
                int[] row = new int[this.Width];
                for (int j = 0; j < row.Length; j++)
                {
                    Tile tile = new Tile(false, false);
                    if (i == character.Position[0] && j == character.Position[1])
                    {
                        tile.IsStartTile = true;
                        row[j] = character.Value;
                    }
                    else if (j == row.Length - 2 && i == mapArray.Length - 2)
                    {
                        tile.IsEndTile = true;
                        row[j] = tile.Value;
                    }
                    else
                    {
                        row[j] = wall.Value;
                    }
                }
                mapArray[i] = row;
            }
            this.MapArrayOfArrays = mapArray;
        }

        /// <summary>
        /// Logic for turning the values within the map into the string for display
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public string BuildMapDisplay(PlayerCharacter character)
        {
            string mapDisplay = "";
            for (int i = 0; i < this.MapArrayOfArrays.Length; i++)
            {
                int[] row = this.MapArrayOfArrays[i];
                for (int j = 0; j < row.Length; j++)
                {
                    Wall wall = new Wall();
                    Tile tile = new Tile(false, false);
                    if (row[j] == wall.Value)
                    {
                        mapDisplay += wall.Display;
                    }
                    else if (row[j] == character.Value)
                    {
                        mapDisplay += character.Display;
                    }
                    else if (row[j] == -3)
                    {
                        mapDisplay += "D";
                    }
                    else if (row[j] == -4)
                    {
                        mapDisplay += "G";
                    }
                    else if (row[j] == -5)
                    {
                        mapDisplay += "H";
                    }
                    else if (row[j] == tile.Value || row[j] == 1 || row[j] == 2)
                    {
                        if (row[j] == 1)
                        {
                            tile.IsStartTile = true;
                        }
                        if (row[j] == 2)
                        {
                            tile.IsEndTile = true;
                        }
                        mapDisplay += tile.Display;
                    }

                }
                mapDisplay += "\n\r";
            }
            return mapDisplay;
        }
    }
}

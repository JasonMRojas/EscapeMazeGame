using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeMazeGame.Classes
{
    class Game
    {
        /// <summary>
        /// Runs the logic around the game loop
        /// </summary>
        /// <param name="difficulty">Difficulty of the game</param>
        /// <param name="newPlayer">The current player</param>
        /// <returns>Whether or not the game will continue</returns>
        public bool RunGame(int difficulty, PlayerCharacter newPlayer)
        {
            bool retry = false;
            do
            {
                newPlayer.Position[0] = 1;
                newPlayer.Position[1] = 1;
                retry = false;
                Console.WriteLine("Loading New Map...");
                Console.WriteLine("This may take up to 60 seconds...");
                int height = 10;
                if (difficulty < 3)
                {
                    height = difficulty * 6;
                }
                int width;
                if (difficulty > 5)
                {
                    width = difficulty * 15;
                    height = difficulty * 2;
                }
                else
                {
                    width = 50;
                }
                Map map = new Map(width, height, newPlayer);
                List<Drone> dronePile = new List<Drone>();
                for (int i = 0; i < difficulty * 2; i++)
                {
                    EnemyDroneRandom newDrone = new EnemyDroneRandom(map);
                    dronePile.Add(newDrone);
                }
                for (int i = 0; i < difficulty / 2; i++)
                {
                    EnemyDroneDigger newDiggerDrone = new EnemyDroneDigger(map);
                    dronePile.Add(newDiggerDrone);
                }
                for (int i = 0; i < difficulty / 10; i++)
                {
                    EnemyDroneDiggerAggressive newDiggerDrone = new EnemyDroneDiggerAggressive(map);
                    dronePile.Add(newDiggerDrone);
                }

                map.Display = map.BuildMapDisplay(newPlayer);
                bool didWin = false;

                didWin = RunGameLoop(difficulty, map, newPlayer, dronePile);
                if (didWin)
                {
                    Console.WriteLine("-----CONGRATS YOU WIN-----");
                    if (difficulty < 10)
                    {
                        Console.WriteLine("There are still challenges left would you like to try the next difficulty (y/n)?");
                        string input = Console.ReadLine().ToLower();
                        if (input == "y")
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Thanks for playing!");
                            Console.ReadLine();
                        }

                    }
                    else
                    {
                        if (difficulty == 10)
                        {
                            Console.WriteLine($"YOU BEAT LEVEL 10... {newPlayer.Name} YOU ARE HEREBY DUBBED RNGESUS!!!");
                        }
                        Console.WriteLine("Thanks for playing!");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("-----GAME OVER-----");
                    Console.WriteLine("Would you like to try again (y/n)?");
                    string input = Console.ReadLine().ToLower();
                    if (input == "y")
                    {
                        retry = true;
                    }
                    else
                    {
                        Console.WriteLine("Thanks for playing!");
                        Console.ReadLine();
                    }
                }
            } while (retry);

            return false;

        }

        /// <summary>
        /// Menu screen logic
        /// </summary>
        public void MenuScreen()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("     Welcome to Drone Dodger");
            Console.WriteLine("----------------------------------");

            Console.Write("Enter your character name: ");
            string name = Console.ReadLine();
            PlayerCharacter newPlayer = new PlayerCharacter(name);
            Console.Write("Enter your difficulty (1-10): ");
            int difficulty = int.Parse(Console.ReadLine());
            if (difficulty < 1 || difficulty > 10)
            {
                Console.WriteLine("No valid difficulty entered, defaulting to medium (5).");
                difficulty = 5;
            }
            Console.CursorVisible = false;
            while (true)
            {
                bool runAgain = RunGame(difficulty, newPlayer);

                if (runAgain)
                {
                    difficulty++;
                }
                else
                {
                    break;
                }
            }


        }

        /// <summary>
        /// Runs the game loop. Moving tokens and otherwise
        /// </summary>
        /// <param name="difficulty">Current Game Difficulty</param>
        /// <param name="map">The Current Map</param>
        /// <param name="newPlayer">The Current Player</param>
        /// <param name="dronePile">All of the enemies currently being placed onto the map</param>
        /// <returns></returns>
        public bool RunGameLoop(int difficulty, Map map, PlayerCharacter newPlayer, List<Drone> dronePile)
        {
            bool winState = false;
            bool gameOver = false;
            Console.Clear();
            DisplayMap(difficulty, map);
            int tickCounter = 0;

            while (!winState && !gameOver) //Runs 
            {
                if (tickCounter % 100 == 0 && tickCounter != 0 && difficulty >= 10) //Dupication logic for Agrgressive Drone Boss
                {
                    EnemyDroneDiggerAggressive newBossDrone = new EnemyDroneDiggerAggressive(map);
                    dronePile.Add(newBossDrone);
                }
                Console.CursorVisible = false;
                newPlayer.Move(map);
                foreach (Drone enemyDrone in dronePile) //Moves each drone which was added to the list of them. 
                {
                    enemyDrone.Move(map);
                }
                map.Display = map.BuildMapDisplay(newPlayer); //Sets the display string to the Built Map display with the new locations.

                DisplayMap(difficulty, map); // Sets the display of the map.

                //Console.WriteLine(map.Display);
                bool hitDrone = true;
                for (int i = 0; i < map.MapArrayOfArrays.Length; i++)
                {
                    for (int j = 0; j < map.MapArrayOfArrays[i].Length; j++)
                    {
                        if (map.MapArrayOfArrays[i][j] == newPlayer.Value)
                        {
                            hitDrone = false; //Checks the entire map for the player value. If the value exists then it did not hit the drone.
                        }
                    }
                }

                if (hitDrone)
                {
                    gameOver = true;
                }

                if (newPlayer.Position[0] == map.MapArrayOfArrays.Length - 2
                    && newPlayer.Position[1] == map.MapArrayOfArrays[map.MapArrayOfArrays.Length - 2].Length - 2) //If the player has made it to the end They win
                {
                    winState = true;
                }
                tickCounter++; //Counts each loop.
            }
            return winState;
        }

        /// <summary>
        /// Runs the logic to display the Map in the console
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="map"></param>
        private void DisplayMap(int difficulty, Map map)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0); //Basically works as buffering
            Console.CursorVisible = false;
            foreach (char letter in map.Display) //Prints out the map one letter at a time. So that we can color certain parts
            {
                if (letter == 'P')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(letter);
                }
                else if (letter == 'D')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(letter);
                }
                else if (letter == 'G')
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(letter);
                }
                else if (letter == 'H')
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(letter);
                }
                else if (letter == 'E')
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(letter);
                }
                else
                {
                    Console.Write(letter);
                }
                Console.ResetColor();
            }

            Console.WriteLine("Controls are arrow keys or (WASD)");
            Console.Write("YOU ARE THE: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("P");
            Console.ResetColor();
            Console.Write("GOAL REACH THE: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("E");
            Console.WriteLine();
            Console.ResetColor();
            if (difficulty >= 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("D");
                Console.ResetColor();
                Console.WriteLine(": are Normal drones they move about trying to find the player... Don't touch them!!!");
            }
            if (difficulty >= 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("G");
                Console.ResetColor();
                Console.WriteLine(": are Digger Drones, they can go through walls and are a little more aggressive than Normal Drones");
            }
            if (difficulty >= 10)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("H");
                Console.ResetColor();
                Console.WriteLine(": is the boss drone. He is aggressive and targeted, And it duplicates if you take too long. Good Luck.");
            }
        }
    }
}

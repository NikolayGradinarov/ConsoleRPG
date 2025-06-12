using ElfshockRPG.Helpers;
using ElfshockRPG.Enums;
using ElfshockRPG.Screens;
using ElfshockRPG.Characters;
using System.Text;

namespace ElfshockRPG.Screens
{
    public class InGame
    {
        // Generate random monster positions
        private static Random rnd = new Random();

        public static async Task RunAsync()
        {
            // Set player's starting position
            GameState.PlayerX = 1;
            GameState.PlayerY = 1;
            Console.Clear();

            // Game loop - continues as long as the players is in the "InGame" state
            while (GameState.CurrentScreen == GameScreen.InGame)
            {
                Console.WriteLine();
                DrawGrid();
                
                Console.WriteLine("Choose action:");
                Console.WriteLine("1) Move");
                Console.WriteLine("2) Attack");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    await HandleMoveAsync();
                }
                else if (action == "2")
                {
                    await HandleAttackAsync();
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    await Task.Delay(1000);
                    continue;
                }

                SpawnMonster();
                MoveMonsters();

                // Check if the player's health had dropped to 0 or below
                if (CheckGameOver())
                {
                    Console.WriteLine("You've Died!");
                    await Task.Delay(3000);
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    await Task.Delay(2000);

                    GameState.CurrentScreen = GameScreen.Exit;
                    await ExitGame.Run();
                }
            }
        }

        // Draws the grid representing the game field
        private static void DrawGrid()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            const int size = GameState.GridSize;
            char[,] field = new char[size, size];

            DisplayStats();
            Console.WriteLine();
            Console.WriteLine();

            // Fill the entire grid with a background character
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    field[y, x] = '▒';
                }   
            }

            // Place the player symbol at the player's coordinates
            field[GameState.PlayerY, GameState.PlayerX] = GameState.PlayerCharacter.Symbol;

            // Place each monster on the grid if their coordinates are valid
            foreach (var monster in GameState.Monsters)
            {
                
                if (monster.X >= 0 && monster.X < size && monster.Y >= 0 && monster.Y < size)
                {
                    field[monster.Y, monster.X] = monster.Symbol;
                }
            }

            // Print the entire grid row by row
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.Write(field[y, x]);
                }
                Console.WriteLine();
            }
        }

        // Display the player's current stats
        private static void DisplayStats()
        {
            Console.WriteLine($"Health: {GameState.PlayerCharacter.Health}  Mana: {GameState.PlayerCharacter.Mana}");
        }

        // Movement input and update player position
        private static async Task HandleMoveAsync()
        {
            // Display movement key options to the player
            StringBuilder sb = new StringBuilder();
            Console.WriteLine();
            sb.AppendLine("W - Move up");
            sb.AppendLine("S - Move down");
            sb.AppendLine("D - Move right");
            sb.AppendLine("A - Move left");
            sb.AppendLine("E - Move diagonally up & right");
            sb.AppendLine("X - Move diagonally down & right");
            sb.AppendLine("Q - Move diagonally up & left");
            sb.AppendLine("Z - Move diagonally down & left");
            Console.WriteLine(sb.ToString().TrimEnd());

            ConsoleKeyInfo key = Console.ReadKey(true);

            int newX = GameState.PlayerX;
            int newY = GameState.PlayerY;

            // Calculate player position based on input
            switch (key.Key)
            {
                case ConsoleKey.W: newY--; break;
                case ConsoleKey.S: newY++; break;
                case ConsoleKey.A: newX--; break;
                case ConsoleKey.D: newX++; break;
                case ConsoleKey.E: newX++; newY--; break;
                case ConsoleKey.X: newX++; newY++; break;
                case ConsoleKey.Q: newX--; newY--; break;
                case ConsoleKey.Z: newX--; newY++; break;
                default:
                    Console.WriteLine("Invalid move key.");
                    await Task.Delay(1500);
                    return;
            }

            // Prevent movement outside the grid boundaries
            if (newX >= 0 && newX < GameState.GridSize && newY >= 0 && newY < GameState.GridSize)
            {
                GameState.PlayerX = newX;
                GameState.PlayerY = newY;
            }
            else
            {
                Console.WriteLine("You cannot move outside the grid.");
                await Task.Delay(900);
            }
            await Task.Delay(100);
        }

        // Handles player attacks
        private static async Task HandleAttackAsync()
        {
            // Find all monsters within the player's attack range
            var targets = GameState.Monsters
                .Where(m => Distance(m.X, m.Y, GameState.PlayerX, GameState.PlayerY) <= GameState.PlayerCharacter.Range)
                .ToList();

            if (!targets.Any())
            {
                Console.WriteLine("No available targets in your range.");
                await Task.Delay(1000);
                return;
            }

            // Display available targets
            Console.WriteLine("Choose target to attack:");
            for (int i = 0; i < targets.Count; i++)
            {
                Console.WriteLine($"{i + 1}) Monster at ({targets[i].X},{targets[i].Y}) with Health: {targets[i].Health}");
            }

            // Ask the player to choose a valid target
            int choice = 0;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= targets.Count)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Try again.");
            }

            var target = targets[choice - 1];
            target.Health -= GameState.PlayerCharacter.Damage;

            // Remove monster if defeated
            if (target.Health <= 0)
            {
                GameState.Monsters.Remove(target);
                Console.WriteLine("Monster defeated!");
            }
            else
            {
                Console.WriteLine($"Monster health is now {target.Health}");
            }
            await Task.Delay(500);
        }

        // Creates a new monster at a random unoccupied position
        private static void SpawnMonster()
        {
            int x, y;

            // Find a valid random position not occupied by player or another monster
            do
            {
                x = rnd.Next(0, GameState.GridSize);
                y = rnd.Next(0, GameState.GridSize);
            }
            while ((x == GameState.PlayerX && y == GameState.PlayerY) || GameState.Monsters.Any(m => m.X == x && m.Y == y));

            GameState.Monsters.Add(new Monster(x, y));
        }

        // Moves each monster towards the player or attacks if nearby 
        private static void MoveMonsters()
        {
            foreach (var monster in GameState.Monsters)
            {
                if (Distance(monster.X, monster.Y, GameState.PlayerX, GameState.PlayerY) == 1)
                {
                    
                    GameState.PlayerCharacter.Health -= monster.Damage;

                    if (GameState.PlayerCharacter.Health >= 0)
                    {
                        Console.WriteLine($"Monster attacked you! Your health is now {GameState.PlayerCharacter.Health}");
                    }  
                }
                else
                {
                    // Move monster closer to the player
                    if (monster.X < GameState.PlayerX)
                    {
                        monster.X++;
                    }

                    else if (monster.X > GameState.PlayerX) 
                    {
                        monster.X--;
                    }

                    if (monster.Y < GameState.PlayerY)
                    {
                        monster.Y++;
                    }

                    else if (monster.Y > GameState.PlayerY) 
                    {
                        monster.Y--;
                    }
                }
            }
        }

        // Returns true if the player is dead
        private static bool CheckGameOver()
        {
            if (GameState.PlayerCharacter.Health <= 0)
            {
                return true;            
            }
            return false;
        }

        // Calculates the distance between two points on the grid
        private static int Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }
    }
}


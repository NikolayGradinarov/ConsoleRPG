using ElfshockRPG.Characters;
using ElfshockRPG.Data;
using ElfshockRPG.Data.Repositories;
using ElfshockRPG.Helpers;
using ElfshockRPG.Enums;

namespace ElfshockRPG.Screens
{
    public class CharacterSelect
    {
        public static async Task Show()
        {
            // Prompt the player to choose a character class
            Console.Clear();
            Console.WriteLine("Choose character type:");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine();
            Console.WriteLine("1) Warrior\n2) Archer\n3) Mage");
            Console.WriteLine();
            Console.Write("Your pick: ");

            // Get a valid user input (1 to 3) by using a helper method
            int pick = InputHelper.GetIntInput(1, 3);

            Character character;

            // Create the chosen character type
            if (pick == 1)
            {
                character = new Warrior();
            }
            else if (pick == 2)
            {
                character = new Archer();
            }
            else if (pick == 3)
            {
                character = new Mage();
            }
            else
            {
                // Safety exception! Shouldn't be reached due to input validation.
                throw new ArgumentException("Invalid pick");
            }

            // Ask the player if they want to boost their stats manually - buff
            Console.Clear();
            Console.WriteLine("Would you like to buff up your stats before starting?         (Limit: 3 points total)");
            Console.Write("Response (Y/N): ");

            // Maximum available points for buffing
            int remaining = 3;

            while (true)
            {
                string response = Console.ReadLine().ToUpper();

                if (response == "Y")
                {
                    while (remaining > 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"Remaining Points: {remaining}");

                        if (remaining > 0)
                        {
                            Console.Write("Add to Strength: ");
                            int str = InputHelper.GetIntInput(0, remaining);
                            character.Strength += str;
                            remaining -= str;
                        }

                        if (remaining > 0)
                        {
                            Console.Write("Add to Agility: ");
                            int agi = InputHelper.GetIntInput(0, remaining);
                            character.Agility += agi;
                            remaining -= agi;
                        }

                        if (remaining > 0)
                        {
                            Console.Write("Add to Intelligence: ");
                            int intl = InputHelper.GetIntInput(0, remaining);
                            character.Intelligence += intl;
                            remaining -= intl;
                        }
                    }
                    break;
                }

                // Skip the stats buffing and continue to the game
                else if (response == "N")
                {
                    GameState.CurrentScreen = GameScreen.InGame;
                    break;
                }
                // Prompt again if input is invalid
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }

            // Run character specific setup login
            character.Setup();
            GameState.PlayerCharacter = character;

            // Persist the character to the database using the repository pattern
            var repo = new CharacterRepository(new ElfshockRPGDbContext());
            await repo.SaveCharacterAsync(character);

            // Move to the InGame screen now that the character is ready
            GameState.CurrentScreen = GameScreen.InGame;
        }
    }
}

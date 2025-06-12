using ElfshockRPG.Screens;
using ElfshockRPG.Characters;
using ElfshockRPG.Enums;

namespace ElfshockRPG
{
    public class GameState
    {
        // Current game screen state
        public static GameScreen CurrentScreen { get; set; } = GameScreen.MainMenu;
        public static Character PlayerCharacter { get; set; }

        // Player's position on the grid
        public static int PlayerX = 1;
        public static int PlayerY = 1;

        // List of monsters on the grid
        public static List<Monster> Monsters = new List<Monster>();

        public const int GridSize = 10;

        // Main game loop that switches screens until game ends
        public async Task RunAsync()
        {
            while (CurrentScreen != GameScreen.Exit)
            {
                switch (CurrentScreen)
                {
                    case GameScreen.MainMenu:
                        await MainMenu.Show();
                        break;
                    case GameScreen.CharacterSelect:
                        await CharacterSelect.Show();
                        break;
                    case GameScreen.InGame:
                        await InGame.RunAsync();
                        break;                   
                }
            }

            await ExitGame.Run();
        }
    }
}

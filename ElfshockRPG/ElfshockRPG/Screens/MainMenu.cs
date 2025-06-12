using ElfshockRPG.Enums;

namespace ElfshockRPG.Screens
{
    public class MainMenu
    {
        public static async Task Show()
        {
            // Displaying Main Menu
            Console.Clear();
            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play.");
            Console.ReadKey();

            GameState.CurrentScreen = GameScreen.CharacterSelect;
        }
    }
}

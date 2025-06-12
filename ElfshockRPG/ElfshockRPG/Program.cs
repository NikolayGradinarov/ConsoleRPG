using ElfshockRPG.Screens;

namespace ElfshockRPG
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Create a new game state instance and start the main game loop asynchronously
            var inGame = new GameState();
            await inGame.RunAsync();
        }
    }
}

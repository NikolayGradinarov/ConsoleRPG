namespace ElfshockRPG.Screens
{
    public class ExitGame
    {
        public static async Task Run()
        {
            // Displaying Exit Game Menu
            Console.Clear();
            Console.WriteLine("Thank You for playing Elfshock RPG!");
            Environment.Exit(0);
        }
    } 
}

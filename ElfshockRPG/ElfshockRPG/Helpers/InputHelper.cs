namespace ElfshockRPG.Helpers
{
    public class InputHelper
    {
        // Reads an integer input from the user within the specified range [min, max]
        // Keeps prompting until the user enters a valid number in the range
        public static int GetIntInput(int min, int max)
        {
            int input;
    
            while (true)
            {
                Console.Write(">> ");
                string line = Console.ReadLine();

                // Try to convert input to an integer
                if (!int.TryParse(line, out input))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                // Check if the number is within the allowed range
                if (input < min || input > max)
                {
                    Console.WriteLine($"Enter a number between {min} and {max}.");
                    continue;
                }

                // Valid input, return
                return input;
            }
        }
    }
}

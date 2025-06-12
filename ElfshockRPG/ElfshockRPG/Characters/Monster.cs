namespace ElfshockRPG.Characters
{
    public class Monster : Character
    {
        // Creates a monster with random base stats and sets its position and symbol
        private static Random random = new Random();

        public int X, Y;

        public Monster(int x, int y)
        {
            Strength = random.Next(1, 3);
            Agility = random.Next(1, 3);
            Intelligence = random.Next(1, 3);
            Range = 1;
            Symbol = '0';
            X = x;
            Y = y;
            Setup();
        }
    }
}

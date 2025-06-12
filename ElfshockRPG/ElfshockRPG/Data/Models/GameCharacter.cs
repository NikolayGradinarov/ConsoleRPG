using System.ComponentModel.DataAnnotations;

namespace ElfshockRPG.Data.Models
{
    // Character entity for database storage
    public class GameCharacter
    {
        [Key]
        public int Id { get; set; } 
        public string Type { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Range { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public char Symbol { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}

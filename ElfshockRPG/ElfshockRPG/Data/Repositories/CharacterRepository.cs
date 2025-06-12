using ElfshockRPG.Characters;
using ElfshockRPG.Data.Models;

namespace ElfshockRPG.Data.Repositories
{
    public class CharacterRepository
    {
        private readonly ElfshockRPGDbContext _context;

        public CharacterRepository(ElfshockRPGDbContext context)
        {
            _context = context;
        }

        public async Task SaveCharacterAsync(Character character)
        {
            // Create a new GameCharacter entity and map properties from Character model
            var entity = new GameCharacter
            {
                Type = character.GetType().Name,
                Strength = character.Strength,
                Agility = character.Agility,
                Intelligence = character.Intelligence,
                Range = character.Range,
                Health = character.Health,
                Mana = character.Mana,
                Damage = character.Damage,
                Symbol = character.Symbol,
                CreatedAt = DateTime.Now
            };

            // Add the new entity to the DbContext's tracking set
            _context.Characters.Add(entity);

            // Commit changes to the database asynchronously
            await _context.SaveChangesAsync();
        }
    }
}
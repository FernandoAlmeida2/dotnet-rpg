using dotnet_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Repositories.CharacterRepository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Character>> getAllCharacters(int userId)
        {
            return await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User!.Id == userId).ToListAsync();
        }

        public async Task<Character?> getCharacterById(int id, int userId)
        {
            return await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == userId);
        }

        public async Task<List<Character>> saveCharacter(Character newCharacter, int userId)
        {
            newCharacter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await _context.Characters
                    .Where(c => c.User!.Id == userId)
                    .ToListAsync();
        }
        public async Task UpdateCharacter(Character character)
        {
            _context.Update(character);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCharacter(Character character)
        {
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }


        public async Task AddCharacterSkill(Character character, Skill skill)
        {
            character.Skills!.Add(skill);
            await _context.SaveChangesAsync();
        }

        public async Task<Skill?> GetSkillById(int skillId)
        {
           return await _context.Skills.FirstOrDefaultAsync(s => s.Id == skillId);
        }
    }
}
using dotnet_rpg.Contexts;
using dotnet_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Repositories.CharacterRepository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IUserContext _userContext;
        private readonly DataContext _context;

        public CharacterRepository(DataContext context, IUserContext userContext)
        {
            _userContext = userContext;
            _context = context;
        }

        public async Task<List<Character>> getAllCharacters()
        {
            return await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User!.Id == _userContext.GetUserId()).ToListAsync();
        }

        public async Task<Character?> getCharacterById(int id)
        {
            return await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == _userContext.GetUserId());
        }

        public async Task<Character?> getAnyCharacterById(int id)
        {
            return await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Character>> saveCharacter(Character newCharacter)
        {
            newCharacter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == _userContext.GetUserId());
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await getAllCharacters();
        }
        public async Task UpdateCharacter(Character character)
        {
            _context.Update(character);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Character>> DeleteCharacter(Character character)
        {
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            
            return await getAllCharacters();
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
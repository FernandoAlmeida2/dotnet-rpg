namespace dotnet_rpg.Repositories.CharacterRepository
{
    public interface ICharacterRepository
    {
        Task<List<Character>> getAllCharacters(int userId);
        Task<Character?> getCharacterById(int id, int userId);
        Task<List<Character>> saveCharacter(Character newCharacter, int userId);
        Task UpdateCharacter(Character character);
        Task DeleteCharacter(Character character);
        Task AddCharacterSkill(Character character, Skill skill);
        Task<Skill?> GetSkillById(int skillId);
    }
}
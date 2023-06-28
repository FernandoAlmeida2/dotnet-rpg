namespace dotnet_rpg.Repositories.CharacterRepository
{
    public interface ICharacterRepository
    {
        Task<List<Character>> getAllCharacters();
        Task<Character?> getCharacterById(int id);
        Task<Character?> getAnyCharacterById(int id);
        Task<List<Character>> saveCharacter(Character newCharacter);
        Task UpdateCharacter(Character character);
        Task<List<Character>> DeleteCharacter(Character character);
        Task AddCharacterSkill(Character character, Skill skill);
        Task<Skill?> GetSkillById(int skillId);
    }
}
namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<Character>> getAllCharacters();

        Task<Character> getCharacterById(int id);

        Task<List<Character>> saveCharacter(Character newCharacter);
    }
}
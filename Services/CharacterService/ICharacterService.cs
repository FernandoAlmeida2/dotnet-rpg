namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        List<Character> getAllCharacters();

        Character getCharacterById(int id);

        List<Character> saveCharacter(Character newCharacter);
    }
}
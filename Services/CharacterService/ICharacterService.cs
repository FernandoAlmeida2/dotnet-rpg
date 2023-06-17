using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> getAllCharacters();

        Task<ServiceResponse<Character>> getCharacterById(int id);

        Task<ServiceResponse<List<Character>>> saveCharacter(Character newCharacter);
    }
}
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResponseDto>>> getAllCharacters();

        Task<ServiceResponse<GetCharacterResponseDto>> getCharacterById(int id);

        Task<ServiceResponse<List<GetCharacterResponseDto>>> saveCharacter(AddCharacterRequestDto newCharacter);
    }
}
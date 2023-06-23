using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<CharacterResponseDto>>> getAllCharacters();
        Task<ServiceResponse<CharacterResponseDto>> getCharacterById(int id);
        Task<ServiceResponse<List<CharacterResponseDto>>> saveCharacter(CharacterRequestDto newCharacter);
        Task<ServiceResponse<CharacterResponseDto>> UpdateCharacter(int id, CharacterRequestDto updateCharacter);
        Task<ServiceResponse<List<CharacterResponseDto>>> DeleteCharacter(int id);
        Task<ServiceResponse<CharacterResponseDto>> AddCharacterSkill(CharacterSkillRequestDto newCharacterSkill);
    }
}
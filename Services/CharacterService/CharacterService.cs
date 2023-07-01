using AutoMapper;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly ICharacterRepository _repository;

        public CharacterService(IMapper mapper, ICharacterRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ServiceResponse<List<CharacterResponseDto>>> getAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var dbCharacters = await _repository.getAllCharacters();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> getCharacterById(int id)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var dbCharacter = await _repository.getCharacterById(id);
                if (dbCharacter is null) throw new Exception("Character not found");

                response.Data = _mapper.Map<CharacterResponseDto>(dbCharacter);
            }
            catch (Exception ex) { response.HandleError(ex.Message); }

            return response;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> saveCharacter(
            CharacterRequestDto newCharacter)
        {
            var response = new ServiceResponse<List<CharacterResponseDto>>();
            var character = new Character(newCharacter.Name, newCharacter.Class);
            var dbCharacters = await _repository.saveCharacter(character);
            response.Data = dbCharacters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            response.Message = "Character added successfully!";
            return response;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> UpdateCharacter(int id,
            CharacterRequestDto updateCharacter)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = await _repository.getCharacterById(id);
                if (character is null) throw new Exception("Character not found");

                await _repository.UpdateCharacter(_mapper.Map(updateCharacter, character));
                response.Data = _mapper.Map<CharacterResponseDto>(character);
                response.Message = "Character updated successfully!";
            }
            catch (Exception ex) { response.HandleError(ex.Message); }

            return response;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<CharacterResponseDto>>();
            try
            {
                var character = await _repository.getCharacterById(id);
                if (character is null) throw new Exception("Character not found");

                var dbCharacters = await _repository.DeleteCharacter(character);
                response.Message = "Character deleted successfully!";
                response.Data = dbCharacters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            }
            catch (Exception ex) { response.HandleError(ex.Message); }

            return response;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> AddCharacterSkill(CharacterSkillRequestDto newCharacterSkill)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var skill = await _repository.GetSkillById(newCharacterSkill.SkillId);
                if (skill is null) throw new Exception("Skill not found");

                var character = await _repository.getCharacterById(newCharacterSkill.CharacterId);

                if (character is null) throw new Exception("Character not found");

                if (character.Skills!.Contains(skill)) throw new Exception(
                    $"{character.Name} already has this skill");

                await _repository.AddCharacterSkill(character, skill);
                response.Data = _mapper.Map<CharacterResponseDto>(character);
                response.Message = $"Skill added to the character {character.Name}";
            }
            catch (Exception ex) { response.HandleError(ex.Message); }

            return response;
        }
    }
}
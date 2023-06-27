using System.Security.Claims;
using AutoMapper;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly ICharacterRepository _repository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public CharacterService(IMapper mapper, ICharacterRepository repository, IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor;
            _mapper = mapper;
            _repository = repository;
        }

        private int GetUserId() => int.Parse(_httpContextAcessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<CharacterResponseDto>>> getAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var dbCharacters = await _repository.getAllCharacters(GetUserId());
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> getCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            var dbCharacter = await _repository.getCharacterById(id, GetUserId());
            serviceResponse.Data = _mapper.Map<CharacterResponseDto>(dbCharacter); ;
            if (dbCharacter is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> saveCharacter(
            CharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            var charactersResponse =  await _repository.saveCharacter(character, GetUserId());
            serviceResponse.Data = charactersResponse
                .Select(c => _mapper.Map<CharacterResponseDto>(c))
                .ToList();
            serviceResponse.Message = "Character added successfully!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> UpdateCharacter(int id, CharacterRequestDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            var character = await _repository.getCharacterById(id, GetUserId());
            if (character is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
            else
            {
                await _repository.UpdateCharacter(_mapper.Map(updateCharacter, character));
                serviceResponse.Data = _mapper.Map<CharacterResponseDto>(character);
                serviceResponse.Message = "Character updated successfully!";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<CharacterResponseDto>>();
            var character = await _repository.getCharacterById(id, GetUserId());

            if (character is null)
            {
                response.Success = false;
                response.Message = "Character not found";
            }
            else
            {
                await _repository.DeleteCharacter(character);
                response.Message = "Character deleted successfully!";
                var dbCharacters = await _repository.getAllCharacters(GetUserId());
                response.Data = dbCharacters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            }

            return response;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> AddCharacterSkill(CharacterSkillRequestDto newCharacterSkill)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var skill = await _repository.GetSkillById(newCharacterSkill.SkillId);
                if (skill is null)
                {
                    response.Success = false;
                    response.Message = "Skill not found";
                    return response;
                }
                var character = await _repository.getCharacterById(newCharacterSkill.CharacterId,
                    GetUserId());

                if (character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                if(character.Skills!.Contains(skill)) {
                    throw new Exception("Character already has this skill");
                }
                await _repository.AddCharacterSkill(character, skill);
                response.Data = _mapper.Map<CharacterResponseDto>(character);
                response.Message = "Skill added to the character " + character.Name;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
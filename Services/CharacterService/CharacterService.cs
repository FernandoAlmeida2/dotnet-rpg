using System.Security.Claims;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAcessor)
        {
            _context = context;
            _httpContextAcessor = httpContextAcessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAcessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<CharacterResponseDto>>> getAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var dbCharacters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> getCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            var dbCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<CharacterResponseDto>(dbCharacter); ;
            if (dbCharacter is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> saveCharacter(CharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data =
                await _context.Characters
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => _mapper.Map<CharacterResponseDto>(c))
                    .ToListAsync();
            serviceResponse.Message = "Character added successfully!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> UpdateCharacter(int id, CharacterRequestDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            var character = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (character is null || character.User!.Id != GetUserId())
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
            else
            {
                _mapper.Map(updateCharacter, character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<CharacterResponseDto>(character);
                serviceResponse.Message = "Character updated successfully!";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());

            if (character is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
            else
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Message = "Character deleted successfully!";
                serviceResponse.Data =
                await _context.Characters
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => _mapper.Map<CharacterResponseDto>(c))
                    .ToListAsync();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> AddCharacterSkill(CharacterSkillRequestDto newCharacterSkill)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill is null)
                {
                    response.Success = false;
                    response.Message = "Skill not found";
                    return response;
                }
                var character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User!.Id == GetUserId());

                if (character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                if(character.Skills!.Contains(skill)) {
                    throw new Exception("Character already has this skill");
                }

                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();

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
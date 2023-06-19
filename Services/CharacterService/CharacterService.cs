using AutoMapper;
using dotnet_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<List<CharacterResponseDto>>> getAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<CharacterResponseDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> getCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
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
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data =
                await _context.Characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToListAsync();
            serviceResponse.Message = "Character added successfully!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> UpdateCharacter(int id, CharacterRequestDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<CharacterResponseDto>();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if (character is null)
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
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

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
            }

            serviceResponse.Data =
                await _context.Characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToListAsync();

            return serviceResponse;
        }
    }
}
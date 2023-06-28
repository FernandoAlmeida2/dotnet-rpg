using System.Security.Claims;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IWeaponRepository _weaponRepository;
        private readonly IMapper _mapper;
        private readonly ICharacterRepository _characterRepository;

        public WeaponService(IWeaponRepository weaponRepository, ICharacterRepository characterRepository, IMapper mapper)
        {
            _weaponRepository = weaponRepository;
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CharacterResponseDto>> AddWeapon(WeaponRequestDto newWeapon)
        {
            var response = new ServiceResponse<CharacterResponseDto>();
            try
            {
                var character = await _characterRepository.getCharacterById(newWeapon.CharacterId);

                if (character is null) throw new Exception("Character not found");

                var weapon = _mapper.Map<Weapon>(newWeapon);
                await _weaponRepository.AddWeapon(weapon);

                response.Data = _mapper.Map<CharacterResponseDto>(character);
                response.Message = "Weapon added successfully!";
            }
            catch (Exception ex) { response.HandleError(ex.Message); }

            return response;
        }
    }
}
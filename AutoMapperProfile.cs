using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterResponseDto>();
            CreateMap<CharacterRequestDto, Character>();
            CreateMap<WeaponRequestDto, Weapon>();
            CreateMap<Weapon, WeaponResponseDto>();
        }
    }
}
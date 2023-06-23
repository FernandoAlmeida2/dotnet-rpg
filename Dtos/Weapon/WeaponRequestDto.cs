
namespace dotnet_rpg.Dtos.Weapon
{
    public class WeaponRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int CharacterId { get; set; }
    }
}
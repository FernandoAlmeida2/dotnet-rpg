namespace dotnet_rpg.Dtos.Character
{
    public class CharacterRequestDto
    {
        public string Name { get; set; } = "Frodo";
        public int? HitPoints { get; set; }
        public int? Strength { get; set; }
        public int? Defense { get; set; }
        public int? Intelligence { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}
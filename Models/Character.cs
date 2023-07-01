namespace dotnet_rpg.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int MaxHP { get; set; }
        public int MindPoints { get; set; }
        public int MaxMP { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; }
        public User? User { get; set; }
        public Weapon? Weapon { get; set; }
        public List<Skill>? Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }

        public Character() : this("Frodo", RpgClass.Knight)
        {}
        public Character(string name, RpgClass rpgClass)
        {
            Name = name;
            Class = rpgClass;

            switch (rpgClass)
            {
                case RpgClass.Mage:
                    HitPoints = MaxHP = 100;
                    MindPoints = MaxMP = 50;
                    Strength = 10;
                    Defense = 10;
                    Intelligence = 30;
                    break;

                case RpgClass.Cleric:
                    HitPoints = MaxHP = 100;
                    MindPoints = MaxMP = 50;
                    Strength = 15;
                    Defense = 15;
                    Intelligence = 20;
                    break;

                case RpgClass.Archer:
                    HitPoints = MaxHP = 100;
                    MindPoints = MaxMP = 30;
                    Strength = 20;
                    Defense = 10;
                    Intelligence = 20;
                    break;

                case RpgClass.Rogue:
                    HitPoints = MaxHP = 100;
                    MindPoints = MaxMP = 30;
                    Strength = 10;
                    Defense = 20;
                    Intelligence = 20;
                    break;

                default:
                    HitPoints = MaxHP = 100;
                    MindPoints = MaxMP = 20;
                    Strength = 20;
                    Defense = 20;
                    Intelligence = 10;
                    break;
            }
        }
    }
}
namespace dotnet_rpg.Repositories.WeaponRepository
{
    public interface IWeaponRepository
    {
        Task AddWeapon(Weapon newWeapon);
    }
}
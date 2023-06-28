namespace dotnet_rpg.Repositories.WeaponRepository
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly DataContext _context;

        public WeaponRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddWeapon(Weapon weapon)
        {
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();
        }
    }
}
using dotnet_rpg.Dtos.Fight;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly ICharacterRepository _characterRepository;
        public FightService(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _characterRepository.getCharacterById(request.AttackerId);
                if (attacker is null || attacker.Skills is null)
                    throw new Exception("Attacker/skill not found");

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if(skill is null) throw new Exception($"{attacker.Name} doesn't know this skill");

                var opponent = await _characterRepository.getAnyCharacterById(request.OpponentId);
                if (opponent is null) throw new Exception("Opponent not found");

                int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0) opponent.HitPoints -= damage;

                if (opponent.HitPoints <= 0) {
                    attacker.Victories++;
                    opponent.Defeats++;
                    response.Message = $"{opponent.Name} has been defeated!";
                    await _characterRepository.UpdateCharacter(attacker);
                }
                    

                await _characterRepository.UpdateCharacter(opponent);

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex) { response.HandleError(ex.Message); }

            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _characterRepository.getCharacterById(request.AttackerId);
                if (attacker is null || attacker.Weapon is null)
                    throw new Exception("Attacker/weapon not found");

                var opponent = await _characterRepository.getAnyCharacterById(request.OpponentId);
                if (opponent is null) throw new Exception("Opponent not found");

                int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0) opponent.HitPoints -= damage;

                if (opponent.HitPoints <= 0) {
                    attacker.Victories++;
                    opponent.Defeats++;
                    response.Message = $"{opponent.Name} has been defeated!";
                    await _characterRepository.UpdateCharacter(attacker);
                }

                await _characterRepository.UpdateCharacter(opponent);

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex) { response.HandleError(ex.Message); }

            return response;
        }
    }
}
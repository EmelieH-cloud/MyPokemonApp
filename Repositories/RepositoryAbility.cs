using FullstackPokemonApp.Database;
using FullstackPokemonApp.Models.DbModels;

namespace FullstackPokemonApp.Repositories
{
    public class RepositoryAbility : IRepositoryAbility
    {
        private readonly MyDbContext _context;

        public RepositoryAbility(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddAbility(AbilityDbModel ability)
        {
            _context.Abilities.Add(ability);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAbility(AbilityDbModel ability)
        {
            _context.Abilities.Update(ability);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAbility(int id)
        {
            var abilityToDelete = await _context.Abilities.FindAsync(id);
            if (abilityToDelete != null)
            {
                _context.Abilities.Remove(abilityToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}

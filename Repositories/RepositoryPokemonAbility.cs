using FullstackPokemonApp.Database;
using FullstackPokemonApp.Models.DbModels;

namespace FullstackPokemonApp.Repositories
{
    public class RepositoryPokemonAbility : IRepositoryPokemonAbility
    {
        private readonly MyDbContext _context;

        public RepositoryPokemonAbility(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddPokemonAbility(PokemonAbilityDbModel pokemonAbility)
        {
            _context.PokemonAbilities.Add(pokemonAbility);
            await _context.SaveChangesAsync();
        }
    }
}

using FullstackPokemonApp.Database;
using FullstackPokemonApp.Models.DbModels;

namespace FullstackPokemonApp.Repositories
{
    public class RepositoryPokemon : IRepositoryPokemon
    {
        private readonly MyDbContext _context;

        public RepositoryPokemon(MyDbContext context)
        {
            _context = context;
        }


        public async Task AddPokemon(PokemonDbModel pokemon)
        {
            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePokemon(PokemonDbModel pokemon)
        {
            _context.Pokemons.Update(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePokemon(int id)
        {
            var pokemonToDelete = await _context.Pokemons.FindAsync(id);
            if (pokemonToDelete != null)
            {
                _context.Pokemons.Remove(pokemonToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
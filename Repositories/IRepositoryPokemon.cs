using FullstackPokemonApp.Models.DbModels;

namespace FullstackPokemonApp.Repositories
{
    public interface IRepositoryPokemon
    {

        Task AddPokemon(PokemonDbModel pokemon);
        Task UpdatePokemon(PokemonDbModel pokemon);
        Task DeletePokemon(int id);
    }
}

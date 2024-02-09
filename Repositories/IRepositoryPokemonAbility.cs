using FullstackPokemonApp.Models.DbModels;

namespace FullstackPokemonApp.Repositories
{
    public interface IRepositoryPokemonAbility
    {
        Task AddPokemonAbility(PokemonAbilityDbModel pokemonAbility);
    }
}

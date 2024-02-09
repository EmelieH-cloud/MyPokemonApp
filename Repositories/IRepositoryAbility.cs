using FullstackPokemonApp.Models.DbModels;

namespace FullstackPokemonApp.Repositories
{
    public interface IRepositoryAbility
    {
        Task AddAbility(AbilityDbModel ability);
        Task UpdateAbility(AbilityDbModel ability);
        Task DeleteAbility(int id);
    }
}

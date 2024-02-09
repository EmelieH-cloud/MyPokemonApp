using FullstackPokemonApp.Models.DbModels;

namespace FullstackPokemonApp.Repositories
{

    public interface IRepositoryType
    {
        Task AddType(TypeDbModel type);
        Task UpdateType(TypeDbModel type);
        Task DeleteType(int id);

    }
}

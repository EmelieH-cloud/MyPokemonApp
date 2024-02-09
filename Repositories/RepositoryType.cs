using FullstackPokemonApp.Database;
using FullstackPokemonApp.Models.DbModels;

namespace FullstackPokemonApp.Repositories
{
    public class RepositoryType : IRepositoryType
    {
        private readonly MyDbContext _context;

        public RepositoryType(MyDbContext context)
        {
            _context = context;
        }


        public async Task AddType(TypeDbModel type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateType(TypeDbModel type)
        {
            _context.Types.Update(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteType(int id)
        {
            var typeToDelete = await _context.Types.FindAsync(id);
            if (typeToDelete != null)
            {
                _context.Types.Remove(typeToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}

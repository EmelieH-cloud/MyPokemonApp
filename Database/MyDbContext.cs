using FullstackPokemonApp.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FullstackPokemonApp.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<PokemonDbModel> Pokemons { get; set; }
        public DbSet<AbilityDbModel> Abilities { get; set; }
        public DbSet<PokemonAbilityDbModel> PokemonAbilities { get; set; }
        public DbSet<TypeDbModel> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-many relation: Pokemon -> Type
            modelBuilder.Entity<PokemonDbModel>()
                .HasOne(p => p.Type)
                .WithMany(t => t.Pokemons)
                .HasForeignKey(p => p.TypeId);

            // Many-to-many relation: Pokemon <-> Ability
            modelBuilder.Entity<PokemonAbilityDbModel>()
                .HasKey(pa => new { pa.PokemonId, pa.AbilityId });

            modelBuilder.Entity<PokemonAbilityDbModel>()
                .HasOne(pa => pa.Pokemon)
                .WithMany(p => p.PokemonAbilities)
                .HasForeignKey(pa => pa.PokemonId);

            modelBuilder.Entity<PokemonAbilityDbModel>()
                .HasOne(pa => pa.Ability)
                .WithMany(a => a.PokemonAbilities)
                .HasForeignKey(pa => pa.AbilityId);
        }
    }
}

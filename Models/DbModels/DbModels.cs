using System.ComponentModel.DataAnnotations;

namespace FullstackPokemonApp.Models.DbModels
{

    /*
     En Pokemon har en typ (one-to-many)
     En Typ kan tillhöra flera Pokemons (one-to-many)
     En Pokemon kan ha flera Abilities (many-to-many)
    En Ability kan tillhöra flera Pokemons (many-to-many)
     */

    public class PokemonDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseExperience { get; set; }
        public int Weight { get; set; }

        // Navigation property for many-to-many relationen med abilities
        public List<PokemonAbilityDbModel> PokemonAbilities { get; set; }
    }

    public class AbilityDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public int Slot { get; set; }

        // Navigation property for many-to-many relationen med pokemon
        public List<PokemonAbilityDbModel> PokemonAbilities { get; set; }
    }

    public class PokemonAbilityDbModel
    {
        [Key]
        public int PokemonId { get; set; }
        public PokemonDbModel Pokemon { get; set; }

        [Key]
        public int AbilityId { get; set; }
        public AbilityDbModel Ability { get; set; }
    }



}

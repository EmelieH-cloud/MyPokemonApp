using Newtonsoft.Json;

namespace FullstackPokemonApp.Models.Roots
{
    // Root för en hel pokemon, se https://pokeapi.co/api/v2/pokemon/1/
    public class PokemonRoot
    {
        [JsonProperty("abilities")]
        public List<Ability> Abilities { get; set; }

        [JsonProperty("base_experience")]
        public int BaseExperience { get; set; }

        [JsonProperty("cries")]
        public Cries Cries { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("types")]
        public List<Type> Types { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }
    }

    public class Cries
    {
        [JsonProperty("latest")]
        public string Latest { get; set; }

        [JsonProperty("legacy")]
        public string Legacy { get; set; }
    }

    public class Ability
    {
        [JsonProperty("ability")]
        public AbilityDetails AbilityDetail { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("slot")]
        public int Slot { get; set; }
    }

    public class AbilityDetails
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Type
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public TypeDetails TypeDetail { get; set; }
    }

    public class TypeDetails
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}


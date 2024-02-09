using FullstackPokemonApp.Models.DbModels;
using FullstackPokemonApp.Models.Roots;
using FullstackPokemonApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FullstackPokemonApp.Pages
{
    public class PokemonDetailsModel : PageModel
    {
        public PokemonRoot? Pokemon { get; set; } // Nu vill vi hämta detaljer om en specifik pokemon från API:et.
        private readonly IRepositoryPokemon _repositoryPokemon;


        public PokemonDetailsModel(IRepositoryPokemon repositoryPokemon)
        {
            _repositoryPokemon = repositoryPokemon;

        }

        public async Task OnGet(string name)
        {
            try
            {
                PokemonRoot response = await new ApiCaller.ApiCaller().MakeCall<PokemonRoot>($"v2/pokemon/{name}");
                Pokemon = response;
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IActionResult> OnPost(string name)
        {
            try
            {
                PokemonRoot response = await new ApiCaller.ApiCaller().MakeCall<PokemonRoot>($"v2/pokemon/{name}");
                Pokemon = response;

                // Skapa en ny Pokemon
                var pokemonDbModel = new PokemonDbModel
                {
                    Name = response.Name,
                    BaseExperience = response.BaseExperience,
                    Weight = response.Weight,
                };

                if (response.Abilities.Any())   // Om det finns några abilities i pokemonens ability-lista...
                {
                    List<PokemonAbilityDbModel> list = new();
                    foreach (var ability in response.Abilities)
                    {
                        // Skapa en db-kopia av varje ability 
                        var abilityDbModel = new AbilityDbModel
                        {
                            Name = ability.AbilityDetail.Name,
                            IsHidden = ability.IsHidden,
                            Slot = ability.Slot
                        };
                        // Skapa en ny PokemonAbilityDbModel
                        var pokemonAbilityDbModel = new PokemonAbilityDbModel();
                        pokemonAbilityDbModel.Ability = abilityDbModel;
                        pokemonAbilityDbModel.Pokemon = pokemonDbModel;
                        pokemonAbilityDbModel.PokemonId = pokemonDbModel.Id;
                        pokemonAbilityDbModel.AbilityId = abilityDbModel.Id;
                        list.Add(pokemonAbilityDbModel);
                    }

                    pokemonDbModel.PokemonAbilities = list;
                }

                // Lägg pokemon i databasen. 
                await _repositoryPokemon.AddPokemon(pokemonDbModel);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // Hantera undantaget här
                return Page();
            }
        }
    }
}
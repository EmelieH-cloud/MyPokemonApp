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

                // Loopa igenom och lägg till typerna för Pokemon
                foreach (var type in response.Types)
                {
                    var typeDbModel = new TypeDbModel
                    {
                        Name = type.TypeDetail.Name
                    };


                    // Skapa en koppling mellan Pokemon och dess typ
                    pokemonDbModel.TypeId = typeDbModel.Id;



                }

                // Loopa igenom och lägg till förmågorna för Pokemon
                foreach (var ability in response.Abilities)
                {
                    var abilityDbModel = new AbilityDbModel
                    {
                        Name = ability.AbilityDetail.Name,
                        IsHidden = ability.IsHidden,
                        Slot = ability.Slot
                    };


                    // Skapa en koppling mellan Pokemon och dess förmåga
                    var pokemonAbilityDbModel = new PokemonAbilityDbModel
                    {
                        PokemonId = pokemonDbModel.Id,
                        AbilityId = abilityDbModel.Id
                    };

                    await _repositoryPokemon.AddPokemon(pokemonDbModel);

                }

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
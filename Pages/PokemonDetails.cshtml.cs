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
        private readonly IRepositoryAbility _repositoryAbility;
        private readonly IRepositoryPokemonAbility _repositoryPokemonAbility;
        private readonly IRepositoryType _repositoryType;

        public PokemonDetailsModel(IRepositoryPokemon repositoryPokemon, IRepositoryAbility repositoryAbility, IRepositoryPokemonAbility repositoryPokemonAbility, IRepositoryType repositoryType)
        {
            _repositoryPokemon = repositoryPokemon;
            _repositoryAbility = repositoryAbility;
            _repositoryPokemonAbility = repositoryPokemonAbility;
            _repositoryType = repositoryType;
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

        public async Task<IActionResult> OnPostAddPokemonToDatabase(string name)
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
                    Weight = response.Weight
                };

                // Lägg till Pokemon i databasen
                await _repositoryPokemon.AddPokemon(pokemonDbModel);

                // Loopa igenom och lägg till typerna för Pokemon
                foreach (var type in response.Types)
                {
                    var typeDbModel = new TypeDbModel
                    {
                        Name = type.TypeDetail.Name
                    };

                    // Lägg till typen i databasen
                    await _repositoryType.AddType(typeDbModel);

                    // Skapa en koppling mellan Pokemon och dess typ
                    pokemonDbModel.TypeId = typeDbModel.Id;

                    // Uppdatera Pokemon i databasen med typen
                    await _repositoryPokemon.UpdatePokemon(pokemonDbModel);
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

                    // Lägg till förmågan i databasen
                    await _repositoryAbility.AddAbility(abilityDbModel);

                    // Skapa en koppling mellan Pokemon och dess förmåga
                    var pokemonAbilityDbModel = new PokemonAbilityDbModel
                    {
                        PokemonId = pokemonDbModel.Id,
                        AbilityId = abilityDbModel.Id
                    };

                    // Lägg till kopplingen i databasen
                    await _repositoryPokemonAbility.AddPokemonAbility(pokemonAbilityDbModel);
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
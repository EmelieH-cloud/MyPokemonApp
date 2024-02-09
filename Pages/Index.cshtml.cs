
using FullstackPokemonApp.Models.Roots;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FullstackPokemonApp.Pages
{
    public class IndexModel : PageModel
    {

        public List<Result> Results { get; set; } // Beh�llare f�r alla list-items som vi vill h�mta via API:et. 


        public async Task OnGet() // OnGet triggas n�r sidan �ppnas. 
        {
            try
            {
                AllPokemonsRoot response = await new ApiCaller.ApiCaller().MakeCall<AllPokemonsRoot>("v2/pokemon/");
                Results = response.Results;
            }
            catch (Exception ex)
            {

            }
        }
    }
}

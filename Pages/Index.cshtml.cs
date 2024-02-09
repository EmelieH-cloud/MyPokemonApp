
using FullstackPokemonApp.Models.Roots;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FullstackPokemonApp.Pages
{
    public class IndexModel : PageModel
    {

        public List<Result> Results { get; set; } // Behållare för alla list-items som vi vill hämta via API:et. 


        public async Task OnGet() // OnGet triggas när sidan öppnas. 
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

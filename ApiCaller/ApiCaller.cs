using Newtonsoft.Json;

namespace FullstackPokemonApp.ApiCaller
{
    public class ApiCaller
    {
        private readonly HttpClient _client;

        public ApiCaller()
        {
            _client = new HttpClient();
            /* HttpClient är en klass i .NET för att skicka och ta emot HTTP-förfrågningar och svar
             från webbserverar. */

            _client.BaseAddress = new Uri("https://pokeapi.co/api/");
            /*basadressen för HTTP-förfrågningar sätts till https://pokeapi.co/api/. 
            Detta innebär att alla förfrågningar som görs med objektet ApiCaller kommer att 
            skickas till denna basadress om ingen annan fullständig URL specificeras.*/
        }


        public async Task<T> MakeCall<T>(string url)
        {
            /* 
            MakeCall är signaturen (namnet på metoden). 
            Task<T> är returtypen för metoden. I det här fallet är T en generisk parameter, vilket betyder att returntypen kan 
             vara vilken typ som helst.
            <T>(string url) definierar in-parameterarna, i detta fall tar metoden in en generisk parameter (skrivs "T") och en sträng 
            (döpt "url"). 
            */

            HttpResponseMessage response = await _client.GetAsync(url);
            /*Denna rad utför en asynkron HTTP GET-förfrågan till den specificerade URL:en med hjälp
             av en HttpClient-instans. */

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                // Om vi lyckades få kontakt med API:et, instansiera en sträng som vi kan spara
                // json-datan i. 

                T? result = JsonConvert.DeserializeObject<T>(json);
                // Denna rad deserialiserar den inlästa JSON-strängen (json) till en objektinstans av
                // den generiska typen T.

                if (result != null)
                {
                    return result;
                }
            }

            throw new HttpRequestException("Failed to retrieve Pokemon data.");
        }
    }
}

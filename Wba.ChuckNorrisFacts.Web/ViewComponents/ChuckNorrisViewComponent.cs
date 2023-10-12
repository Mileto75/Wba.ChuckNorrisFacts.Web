using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wba.ChuckNorrisFacts.Web.ViewModels;

namespace Wba.ChuckNorrisFacts.Web.ViewComponents
{
    [ViewComponent(Name = "FactViewComponent")]
    public class ChuckNorrisViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _url;

        public ChuckNorrisViewComponent()
        {
            _httpClient = new HttpClient();
            _url = new Uri("https://api.chucknorris.io/jokes/random");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //call the api
            var result  = await _httpClient.GetAsync(_url);
            //get the stringified data
            
            var content = await result.Content.ReadAsStringAsync();
            //deserialize json data
            //put in the model
            var chuckNorrisFactViewModel = 
                JsonConvert.DeserializeObject<ChuckNorrisFactViewModel>(content);
            //pass to the view
            return View(chuckNorrisFactViewModel);
        }
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public IEnumerable<string> News { get; private set; }

    public IndexModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task OnGetAsync()
    {
        var client = _clientFactory.CreateClient();
        var response = await client.GetStringAsync("https://newsapi.org/v2/everything?q=DevOps&apiKey=413b5126c97b407395eb0bac6bb8fc81");

        var json = JObject.Parse(response);
        News = json["articles"].ToObject<List<JToken>>().ConvertAll(article => article["title"].ToString());
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DevOpsNewsApp.Services
{
    public class NewsService
    {
        private readonly HttpClient? _httpClient;
        private readonly string? _apiKey;

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NewsAPIKey"];
        }

        public async Task<IEnumerable<NewsStory>> GetDevOpsNewsAsync()
        {
            var response = await _httpClient.GetAsync($"https://newsapi.org/v2/everything?q=devops&apiKey={_apiKey}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var newsResponse = JsonSerializer.Deserialize<NewsApiResponse>(responseBody);

            return newsResponse?.Articles;
        }
    }

    public class NewsApiResponse
    {
        public IEnumerable<NewsStory>? Articles { get; set; }
    }

    public class NewsStory
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
    }
}

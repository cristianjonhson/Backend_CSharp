using Backend.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class PostsService : IPostsService
    {
        private HttpClient _httpClient;

        public PostsService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<PostDto>> GetPosts()
        {
            string url = "https://jsonplaceholder.typicode.com/posts";
            var result = await _httpClient.GetAsync(url);
            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var posts = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);

            return posts;
        }
    }
}

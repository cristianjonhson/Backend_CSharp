using Backend.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class PostsService : IPostsService
    {
        private readonly HttpClient _httpClient;

        // Constructor que recibe una instancia de HttpClient como parámetro
        public PostsService(HttpClient httpClient)
        {
            _httpClient = httpClient;  // Asigna la instancia de HttpClient proporcionada al campo privado
        }

        public async Task<IEnumerable<PostDto>> GetPosts()
        {
            // Realiza una solicitud GET asincrónica a la URL base del HttpClient
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);

            // Lee el contenido de la respuesta como una cadena de manera asincrónica
            var body = await result.Content.ReadAsStringAsync();

            // Opciones de configuración para el deserializador JSON
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            // Deserializa la cadena JSON en una colección de objetos PostDto
            var posts = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);

            return posts;  // Devuelve la colección de objetos PostDto
        }
    }
}

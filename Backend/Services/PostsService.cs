using Backend.DTOs;  // Importa el espacio de nombres que contiene la clase PostDto
using System.Text.Json;  // Importa el espacio de nombres que contiene JsonSerializer


namespace Backend.Services
{
    // Implementación de la interfaz IPostsService
    public class PostsService : IPostsService
    {
        private HttpClient _httpClient;  // Campo para almacenar una instancia de HttpClient

        // Constructor de la clase
        public PostsService()
        {
            _httpClient = new HttpClient();  // Crea una nueva instancia de HttpClient
        }

        // Implementación del método de la interfaz IPostsService
        public async Task<IEnumerable<PostDto>> GetPosts()
        {
            string url = "https://jsonplaceholder.typicode.com/posts";  // URL de la fuente de datos remota

            // Realiza una solicitud GET asincrónica a la URL especificada y espera la respuesta
            var result = await _httpClient.GetAsync(url);

            // Lee el contenido de la respuesta como una cadena de manera asincrónica
            var body = await result.Content.ReadAsStringAsync();

            // Opciones de configuración para el deserializador JSON
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,  // Ignora la distinción entre mayúsculas y minúsculas al deserializar
            };

            // Deserializa la cadena JSON en una colección de objetos PostDto
            var posts = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);

            return posts;  // Devuelve la colección de objetos PostDto
        }
    }
}

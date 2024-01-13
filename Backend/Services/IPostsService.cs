using Backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    // Definición de la interfaz IPostsService
    public interface IPostsService
    {
        // Declaración del método GetPosts
        // Este método devuelve una tarea que representa una colección de objetos PostDto
        Task<IEnumerable<PostDto>> GetPosts();
    }
}

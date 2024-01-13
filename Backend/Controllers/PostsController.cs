using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // Se declara una variable privada para almacenar la instancia de IPostsService.
        private readonly IPostsService _titleService;

        // Constructor que recibe una instancia de IPostsService mediante inyección de dependencias.
        public PostsController(IPostsService titleService)
        {
            // Se asigna la instancia de IPostsService a la variable privada.
            _titleService = titleService;
        }

        // Endpoint HTTP GET para obtener la lista de publicaciones.
        [HttpGet]
        public async Task<IEnumerable<PostDto>> GetPosts()
        {
            try
            {
                // El uso de "async" y "await" permite que este método no bloquee el hilo de ejecución mientras espera el resultado.
                // Mientras se espera la respuesta del servicio, el hilo de ejecución puede ser liberado para manejar otras tareas.

                // Llama al método asincrónico GetPosts del servicio para obtener las publicaciones de forma asíncrona.
                var posts = await _titleService.GetPosts();

                // Después de que se completa la operación asincrónica, el control vuelve a este punto y continúa con la siguiente línea.

                // El resultado del endpoint es la lista de publicaciones, que se serializa automáticamente a JSON antes de ser devuelta al cliente.
                return posts;
            }
            catch (Exception ex)
            {
                // En caso de un error, se podría manejar de manera adecuada, loguear, o devolver un código de error.
                // Aquí solo se muestra un comentario indicando que se puede agregar lógica adicional según sea necesario.
                // También podrías devolver un ActionResult con un código de estado 500 Internal Server Error.
                // Log.Error(ex, "Error al obtener las publicaciones");
                // return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");

                // En este ejemplo, simplemente se relanza la excepción para obtener información de depuración.
                throw;
            }
        }
    }
}

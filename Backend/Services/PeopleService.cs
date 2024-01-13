using Backend.Controllers;

namespace Backend.Services
{
    // Servicio que implementa la interfaz IPeopleService
    public class PeopleService : IPeopleService
    {
        // Método para validar una persona
        public bool Validate(People people)
        {
            // Verifica si el nombre de la persona es nulo o vacío
            if (string.IsNullOrEmpty(people.Name))
            {
                // Si es nulo o vacío, devuelve false (no válido)
                return false;
            }
            // Si el nombre no es nulo o vacío, devuelve true (válido)
            return true;
        }
    }
}

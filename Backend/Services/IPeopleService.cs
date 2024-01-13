using Backend.Controllers;

namespace Backend.Services
{
    // Interfaz que define el contrato para el servicio PeopleService
    public interface IPeopleService
    {
        // Método para validar una persona
        bool Validate(People people);
    }
}

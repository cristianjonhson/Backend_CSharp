using Backend.DTOs; // Importa los namespaces necesarios
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    // Define una interfaz llamada IBeerService que representa las operaciones CRUD para entidades de cerveza
    public interface IBeerService
    {
        // Método para obtener todas las cervezas desde el sistema de almacenamiento
        Task<IEnumerable<BeerDto>> Get();

        // Método para obtener una cerveza específica por su identificador único (ID)
        Task<BeerDto> GetById(int id);

        // Método para agregar una nueva cerveza al sistema
        // Recibe un objeto BeerInsertDto con los detalles de la nueva cerveza
        Task<BeerDto> Add(BeerInsertDto beerInsertDto);

        // Método para actualizar los detalles de una cerveza existente por su ID
        // Recibe el ID de la cerveza a actualizar y un objeto BeerUpdateDto con los nuevos detalles
        Task<BeerDto> UpdateBeer(int id, BeerUpdateDto beerUpdateDto);
    }
}

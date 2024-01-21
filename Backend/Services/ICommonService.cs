// Interfaz genérica que especifica operaciones comunes CRUD para cualquier entidad.
using Microsoft.AspNetCore.Mvc;

public interface ICommonService<T,TI,TU>
{
    // Obtener todos los elementos de la entidad.
    Task<IEnumerable<T>> Get();

    // Obtener un elemento por su ID.
    Task<T> GetById(int id);

    // Agregar un nuevo elemento a la entidad.
    Task<T> Add(TI entityInsertDto);

    // Actualizar un elemento de la entidad por su ID.
    Task<IActionResult> Update(int id, TU entityUpdateDto);
}

using Microsoft.AspNetCore.Mvc;

namespace Backend.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        // Obtener un elemento por su ID.
        Task<TEntity> GetById(int id);

        // Agregar un nuevo elemento a la entidad.
        Task<TEntity> Add(TEntity entity);

        // Actualizar un elemento de la entidad por su ID.
        Task<IActionResult> Update(int id, TEntity entity);

        Task Save();
    }


}
}

using Microsoft.AspNetCore.Mvc;


namespace Backend.Repository
{
    public interface IBrandRepository<T>
    {
        Task<IEnumerable<T>> GetAllBrands();

        Task<T?> GetBrandById(long id);

        Task<T> Add(T entity);

        Task<IActionResult> Update(long id, T entity);

        Task Save();
    }
}

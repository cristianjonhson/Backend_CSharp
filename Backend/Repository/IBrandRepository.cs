using Microsoft.AspNetCore.Mvc;


namespace Backend.Repository
{
    public interface IBrandRepository<T>
    {
        Task<IEnumerable<T>> GetAllBrands();

        Task<T?> GetBrandById(int id);

        Task<T> Add(T entity);

        Task<IActionResult> Update(int id, T entity);

        Task Save();
    }
}

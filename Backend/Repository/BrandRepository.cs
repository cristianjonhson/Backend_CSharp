using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class BrandRepository : IBrandRepository<Brand>
    {
        private readonly StoreContext _storeContext;

        public BrandRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        // Agrega una nueva marca a la base de datos
        public async Task<Brand> Add(Brand entity)
        {
            _storeContext.Brands.Add(entity);
            await Save(); // Guarda los cambios en la base de datos
            return entity;
        }

        // Obtiene todas las marcas de la base de datos
        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await _storeContext.Brands.ToListAsync();
        }

        // Obtiene una marca por su ID
        public async Task<Brand?> GetBrandById(int id)
        {
            return await _storeContext.Brands.FindAsync(id);
        }

        // Guarda los cambios en la base de datos
        public async Task Save()
        {
            await _storeContext.SaveChangesAsync();
        }

        // Actualiza una marca existente por su ID
        public async Task<IActionResult> Update(int id, Brand entity)
        {
            var existingBrand = await _storeContext.Brands.FindAsync(id);

            if (existingBrand == null)
            {
                return new NotFoundResult(); // Retorna NotFound si la marca no existe
            }

            // Actualiza las propiedades según tus necesidades
            existingBrand.Name = entity.Name;
            existingBrand.Description = entity.Description;

            await Save(); // Guarda los cambios en la base de datos

            return new NoContentResult(); // Retorna NoContent indicando que la actualización fue exitosa
        }
    }
}

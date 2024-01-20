// Importa los namespaces necesarios
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class BrandService : IBrandService
    {
        private readonly StoreContext _storeContext;

        // Constructor que recibe el contexto de la base de datos mediante inyección de dependencias
        public BrandService(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrands()
        {
            // Obtiene todas las marcas de la base de datos y proyecta los resultados en BrandDto
            var brands = await _storeContext.Brands
                .Select(x => new BrandDto
                {
                    BrandId = x.BrandId,
                    Name = x.Name,
                    Description = x.Description
                })
                .ToListAsync();

            // Retorna la lista de BrandDto
            return brands;
        }

        public async Task<BrandDto?> GetBrandById(long id)
        {
            // Busca la marca en la base de datos por su ID
            var brand = await _storeContext.Brands.FindAsync(id);

            // Si no se encuentra la marca, retorna null
            if (brand == null)
            {
                return null;
            }

            // Retorna un objeto BrandDto basado en la marca encontrada
            return new BrandDto
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Description = brand.Description
            };
        }

        public async Task<BrandDto> AddBrand(BrandInsertDto brandInsertDto)
        {
            // Crea una nueva instancia de Brand usando los datos del DTO
            var brand = new Brand
            {
                Name = brandInsertDto.Name,
                Description = brandInsertDto.Description
            };

            // Agrega la nueva marca al contexto de la base de datos
            await _storeContext.Brands.AddAsync(brand);

            // Guarda los cambios en la base de datos
            await _storeContext.SaveChangesAsync();

            // Retorna un objeto BrandDto basado en la nueva marca creada
            return new BrandDto
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Description = brand.Description
            };
        }

        public async Task<IActionResult> UpdateBrand(long id, BrandUpdateDto brandUpdateDto)
        {
            // Busca la marca en la base de datos por su ID
            var brand = await _storeContext.Brands.FindAsync(id);

            // Si no se encuentra la marca, retorna un resultado NotFound
            if (brand == null)
            {
                return new NotFoundResult();
            }

            // Actualiza los datos de la marca con los proporcionados en el DTO
            brand.Name = brandUpdateDto.Name;
            brand.Description = brandUpdateDto.Description;

            // Guarda los cambios en la base de datos
            await _storeContext.SaveChangesAsync();

            // Retorna un resultado NoContent indicando que la actualización fue exitosa
            return new NoContentResult();
        }
    }
}

using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository<Brand> _brandRepository;

        // Constructor que recibe el repositorio de marcas mediante inyección de dependencias
        public BrandService(IBrandRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrands()
        {
            // Obtiene todas las marcas a través del repositorio y proyecta los resultados en BrandDto
            var brands = await _brandRepository.GetAllBrands();
            var brandDtos = MapBrandsToDtos(brands);

            // Retorna la lista de BrandDto
            return brandDtos;
        }

        public async Task<BrandDto?> GetBrandById(long id)
        {
            // Obtiene la marca por su ID a través del repositorio
            var brand = await _brandRepository.GetBrandById((int)id);

            // Si no se encuentra la marca, retorna null
            if (brand == null)
            {
                return null;
            }

            // Retorna un objeto BrandDto basado en la marca encontrada
            return MapBrandToDto(brand);
        }

        public async Task<BrandDto> AddBrand(BrandInsertDto brandInsertDto)
        {
            // Crea una nueva instancia de Brand usando los datos del DTO
            var brand = new Brand
            {
                Name = brandInsertDto.Name,
                Description = brandInsertDto.Description
            };

            // Agrega la nueva marca a través del repositorio
            var addedBrand = await _brandRepository.Add(brand);

            // Retorna un objeto BrandDto basado en la nueva marca creada
            return MapBrandToDto(addedBrand);
        }

        public async Task<IActionResult> UpdateBrand(long id, BrandUpdateDto brandUpdateDto)
        {
            // Obtiene la marca por su ID a través del repositorio
            var brand = await _brandRepository.GetBrandById((int)id);

            // Si no se encuentra la marca, retorna un resultado NotFound
            if (brand == null)
            {
                return new NotFoundResult();
            }

            // Actualiza los datos de la marca con los proporcionados en el DTO
            brand.Name = brandUpdateDto.Name;
            brand.Description = brandUpdateDto.Description;

            // Actualiza la marca a través del repositorio
            await _brandRepository.Update((int)id, brand);

            // Retorna un resultado NoContent indicando que la actualización fue exitosa
            return new NoContentResult();
        }

        // Método auxiliar para mapear una colección de marcas a BrandDto
        private IEnumerable<BrandDto> MapBrandsToDtos(IEnumerable<Brand> brands)
        {
            return brands.Select(brand => MapBrandToDto(brand));
        }

        // Método auxiliar para mapear una marca a BrandDto
        private BrandDto MapBrandToDto(Brand brand)
        {
            return new BrandDto
            {
                BrandId = brand.BrandId,
                Name = brand.Name,
                Description = brand.Description
            };
        }
    }
}

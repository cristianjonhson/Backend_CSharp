using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrands();
        Task<BrandDto?> GetBrandById(long id);
        Task<BrandDto> AddBrand(BrandInsertDto brandInsertDto);
        Task<IActionResult> UpdateBrand(long id, BrandUpdateDto brandUpdateDto);

    }
}

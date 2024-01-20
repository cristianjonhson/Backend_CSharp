// Importa los namespaces necesarios
using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

// Especifica la ruta base para las acciones en este controlador
[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    // Constructor que recibe el servicio de marcas mediante inyección de dependencias
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    // Acción HTTP GET para obtener todas las marcas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
    {
        // Delega la responsabilidad al servicio de marcas
        var brandsDto = await _brandService.GetAllBrands();

        // Retorna un resultado Ok con la lista de marcas
        return Ok(brandsDto);
    }

    // Acción HTTP POST para agregar una nueva marca
    [HttpPost]
    public async Task<ActionResult<BrandDto>> AddBrand(BrandInsertDto brandInsertDto)
    {
        // Delega la responsabilidad al servicio de marcas
        var result = await _brandService.AddBrand(brandInsertDto);

        // Retorna un resultado CreatedAtAction con la nueva marca
        return CreatedAtAction(nameof(GetBrandById), new { id = result.BrandId }, result);
    }

    // Acción HTTP GET para obtener una marca por su ID
    [HttpGet("{id}")]
    public async Task<ActionResult<BrandDto>> GetBrandById(long id)
    {
        // Delega la responsabilidad al servicio de marcas
        var brandDto = await _brandService.GetBrandById(id);

        // Si no se encuentra la marca, retorna un resultado NotFound
        if (brandDto == null)
        {
            return NotFound();
        }

        // Retorna un resultado Ok con la marca encontrada
        return Ok(brandDto);
    }

    // Acción HTTP PUT para actualizar una marca por su ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(long id, BrandUpdateDto brandUpdateDto)
    {
        // Delega la responsabilidad al servicio de marcas
        var result = await _brandService.UpdateBrand(id, brandUpdateDto);

        // Retorna el resultado proporcionado por el servicio
        return result;
    }
}

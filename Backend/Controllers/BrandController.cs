// Importa los namespaces necesarios
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// Especifica la ruta base para las acciones en este controlador
[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    // Almacena el contexto de la base de datos
    private StoreContext _storeContext;

    // Constructor que recibe el contexto de la base de datos mediante inyección de dependencias
    public BrandController(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    // Acción HTTP POST para agregar una nueva marca
    [HttpPost]
    public async Task<ActionResult<Brand>> AddBrand(BrandInsertDto brandInsertDto)
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

        // Retorna un resultado CreatedAtAction con la nueva marca
        return CreatedAtAction(nameof(GetBrandById), new { id = brand.BrandId }, brand);
    }

    // Acción HTTP GET para obtener todas las marcas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrands()
    {
        // Obtiene todas las marcas de la base de datos
        var brands = await _storeContext.Brands.ToListAsync();

        // Retorna un resultado Ok con la lista de marcas
        return Ok(brands);
    }

    // Acción HTTP GET para obtener una marca por su ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrandById(long id)
    {
        // Busca la marca en la base de datos por su ID
        var brand = await _storeContext.Brands.FindAsync(id);

        // Si no se encuentra la marca, retorna un resultado NotFound
        if (brand == null)
        {
            return NotFound();
        }

        // Retorna un resultado Ok con la marca encontrada
        return Ok(brand);
    }

    // Acción HTTP PUT para actualizar una marca por su ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(long id, BrandUpdateDto brandUpdateDto)
    {
        // Busca la marca en la base de datos por su ID
        var brand = await _storeContext.Brands.FindAsync(id);

        // Si no se encuentra la marca, retorna un resultado NotFound
        if (brand == null)
        {
            return NotFound();
        }

        // Actualiza los datos de la marca con los proporcionados en el DTO
        brand.Name = brandUpdateDto.Name;
        brand.Description = brandUpdateDto.Description;

        // Guarda los cambios en la base de datos
        await _storeContext.SaveChangesAsync();

        // Retorna un resultado NoContent indicando que la actualización fue exitosa
        return NoContent();
    }
}

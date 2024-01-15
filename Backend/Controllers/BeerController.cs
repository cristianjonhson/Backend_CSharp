// Importa los namespaces necesarios
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Especifica la ruta base para las acciones en este controlador
[Route("api/[controller]")]
[ApiController]
public class BeerController : ControllerBase
{
    // Almacena el contexto de la base de datos
    private StoreContext _storeContext;

    // Constructor que recibe el contexto de la base de datos mediante inyección de dependencias
    public BeerController(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    // Acción HTTP GET para obtener todas las cervezas
    [HttpGet]
    public async Task<IEnumerable<BeerDto>> Get()
    {
        // Realiza una consulta para seleccionar todas las cervezas y proyecta los resultados en BeerDto
        var beerDtos = await _storeContext.Beers
            .Select(x => new BeerDto
            {
                BeerId = x.BeerId,
                BeerDescription = x.BeerDescription,
                BeerName = x.BeerName,
                BeerType = x.BeerType,
                Alcohol = x.Alcohol,
                BrandId = x.Brand.BrandId
            })
            .ToListAsync();

        // Retorna la lista de BeerDto
        return beerDtos;
    }

    // Acción HTTP GET para obtener una cerveza por su ID
    [HttpGet("{id}")]
    public async Task<ActionResult<BeerDto>> GetById(int id)
    {
        // Incluye la propiedad de navegación Brand al cargar la cerveza
        var beer = await _storeContext.Beers
            .Include(b => b.Brand)
            .FirstOrDefaultAsync(b => b.BeerId == id);

        // Si no se encuentra la cerveza, retorna un resultado NotFound
        if (beer == null)
        {
            return NotFound();
        }

        // Crea un objeto BeerDto basado en la cerveza encontrada
        var beerDto = new BeerDto()
        {
            BeerId = beer.BeerId,
            BeerDescription = beer.BeerDescription,
            BeerName = beer.BeerName,
            BeerType = beer.BeerType,
            Alcohol = beer.Alcohol,
            BrandId = beer.Brand != null ? beer.Brand.BrandId : 0
        };

        // Retorna un resultado Ok con el objeto BeerDto
        return Ok(beerDto);
    }

    [HttpPost]
    public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
    {
        // Crea una nueva instancia de la clase Beer
        var beer = new Beer()
        {
            BeerDescription = beerInsertDto.BeerDescription,
            BeerName = beerInsertDto.BeerName,
            BeerType = beerInsertDto.BeerType,
            Alcohol = beerInsertDto.Alcohol,
        };

        // Busca la marca por su ID en la base de datos
        var brand = await _storeContext.Brands.FindAsync(beerInsertDto.BrandId);

        // Si la marca no se encuentra, retorna un resultado NotFound
        if (brand == null)
        {
            return NotFound("Brand not found");
        }

        // Asigna la marca a la cerveza
        beer.Brand = brand;

        // Agrega la cerveza al contexto y guarda los cambios en la base de datos
        await _storeContext.Beers.AddAsync(beer);
        await _storeContext.SaveChangesAsync();

        // Crea un objeto BeerDto basado en la cerveza creada
        var beerDto = new BeerDto
        {
            BeerId = beer.BeerId,
            BeerDescription = beer.BeerDescription,
            BeerName = beer.BeerName,
            BeerType = beer.BeerType,
            Alcohol = beer.Alcohol,
            BrandId = beer.Brand.BrandId
        };

        // Retorna un resultado CreatedAtAction con el objeto BeerDto y la ruta del método GetById
        return CreatedAtAction(nameof(GetById), new { id = beer.BeerId }, beerDto);
    }

}

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
        // Busca la cerveza en la base de datos por su ID
        var beer = await _storeContext.Beers.FindAsync(id);

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
            BrandId = beer.Brand.BrandId
        };

        // Retorna un resultado Ok con el objeto BeerDto
        return Ok(beerDto);
    }
}

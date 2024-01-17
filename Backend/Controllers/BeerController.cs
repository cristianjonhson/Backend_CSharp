﻿// Importa los namespaces necesarios
using Backend.DTOs;
using Backend.Models;
using FluentValidation; // Importa FluentValidation
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Especifica la ruta base para las acciones en este controlador
[Route("api/[controller]")]
[ApiController]
public class BeerController : ControllerBase
{
    // Almacena el contexto de la base de datos
    private StoreContext _storeContext;

    private IValidator<BeerInsertDto> _beerInsertValidator; // Instancia del validador

    // Constructor que recibe el contexto de la base de datos y el validador mediante inyección de dependencias
    public BeerController(StoreContext storeContext, IValidator<BeerInsertDto> beerInsertValidator)
    {
        _storeContext = storeContext;
        _beerInsertValidator = beerInsertValidator; // Asigna el validador
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

    // Acción HTTP POST para agregar una nueva cerveza
    [HttpPost]
    public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
    {
        // Validación usando FluentValidation
        var validatorResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

        // Si la validación no es exitosa, retorna un resultado BadRequest con los errores de validación
        if (!validatorResult.IsValid)
        {
            return BadRequest(validatorResult.Errors);
        }

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

    // Acción HTTP PUT para actualizar una cerveza por su ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBeer(int id, BeerUpdateDto beerUpdateDto)
    {
        // Busca la cerveza en la base de datos por su ID
        var beer = await _storeContext.Beers.FindAsync(id);

        // Si no se encuentra la cerveza, retorna un resultado NotFound
        if (beer == null)
        {
            return NotFound();
        }

        // Actualiza las propiedades de la cerveza con los datos del DTO
        beer.BeerName = beerUpdateDto.BeerName;
        beer.BeerDescription = beerUpdateDto.BeerDescription;
        beer.BeerType = beerUpdateDto.BeerType;
        beer.Alcohol = beerUpdateDto.Alcohol;

        // Verifica si BrandId es mayor que cero (o el valor predeterminado para long)
        if (beerUpdateDto.BrandId > 0)
        {
            var brand = await _storeContext.Brands.FindAsync(beerUpdateDto.BrandId);

            // Si la marca no se encuentra, retorna un resultado NotFound
            if (brand == null)
            {
                return NotFound("Brand not found");
            }

            // Asigna la marca actualizada a la cerveza
            beer.Brand = brand;
        }

        // Guarda los cambios en la base de datos
        await _storeContext.SaveChangesAsync();

        // Retorna un resultado NoContent indicando que la actualización fue exitosa
        return NoContent();
    }
}

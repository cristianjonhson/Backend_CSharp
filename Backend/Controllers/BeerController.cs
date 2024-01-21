// Importa los namespaces necesarios
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
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
    private readonly IBeerService _beerService;

    // Instancia del validador
    private readonly IValidator<BeerInsertDto> _beerInsertValidator;

    // Constructor que recibe el servicio de cervezas, el contexto de la base de datos y el validador mediante inyección de dependencias
    public BeerController(IBeerService beerService, IValidator<BeerInsertDto> beerInsertValidator)
    {
        _beerService = beerService;
        _beerInsertValidator = beerInsertValidator;
    }

    // Acción HTTP GET para obtener todas las cervezas
    [HttpGet]
    public async Task<IEnumerable<BeerDto>> Get()
    {
        // Delega la responsabilidad al servicio de cervezas
        var beerDtos = await _beerService.Get();

        // Retorna la lista de BeerDto
        return beerDtos;
    }

    // Acción HTTP GET para obtener una cerveza por su ID
    [HttpGet("{id}")]
    public async Task<ActionResult<BeerDto>> GetById(int id)
    {
        // Delega la responsabilidad al servicio de cervezas
        var beerDto = await _beerService.GetById(id);

        // Si no se encuentra la cerveza, retorna un resultado NotFound
        if (beerDto == null)
        {
            return NotFound();
        }

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

        // Delega la responsabilidad al servicio de cervezas
        var result = await _beerService.Add(beerInsertDto);

        // Retorna un resultado CreatedAtAction con el objeto BeerDto y la ruta del método GetById
        return CreatedAtAction(nameof(GetById), new { id = result.BeerId }, result);
    }

    // Acción HTTP PUT para actualizar una cerveza por su ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBeer(int id, BeerUpdateDto beerUpdateDto)
    {
        // Delega la responsabilidad al servicio de cervezas
        var result = await _beerService.UpdateBeer(id, beerUpdateDto);

        // Retorna el resultado proporcionado por el servicio
        return (IActionResult)result;
    }
}
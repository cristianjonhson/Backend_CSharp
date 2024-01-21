// Controlador para las operaciones relacionadas con las cervezas.
using Backend.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BeerController : ControllerBase
{
    private readonly ICommonService<BeerDto,BeerInsertDto,BeerUpdateDto> _beerService;
    private readonly IValidator<BeerInsertDto> _beerInsertValidator;

    // Constructor que recibe servicios mediante inyección de dependencias.
    public BeerController(
        [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService,
        IValidator<BeerInsertDto> beerInsertValidator)
    {
        _beerService = beerService;
        _beerInsertValidator = beerInsertValidator;
    }

    // Acción HTTP GET para obtener todas las cervezas.
    [HttpGet]
    public async Task<IEnumerable<BeerDto>> Get()
    {
        // Delega la responsabilidad al servicio de cervezas.
        var beerDtos = await _beerService.Get();

        // Retorna la lista de BeerDto.
        return beerDtos;
    }

    // Acción HTTP GET para obtener una cerveza por su ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<BeerDto>> GetById(int id)
    {
        // Delega la responsabilidad al servicio de cervezas.
        var beerDto = await _beerService.GetById(id);

        // Si no se encuentra la cerveza, retorna un resultado NotFound.
        if (beerDto == null)
        {
            return NotFound();
        }

        // Retorna un resultado Ok con el objeto BeerDto.
        return Ok(beerDto);
    }

    // Acción HTTP POST para agregar una nueva cerveza.
    [HttpPost]
    public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
    {
        // Validación usando FluentValidation.
        var validatorResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

        // Si la validación no es exitosa, retorna un resultado BadRequest con los errores de validación.
        if (!validatorResult.IsValid)
        {
            return BadRequest(validatorResult.Errors);
        }

        // Delega la responsabilidad al servicio de cervezas.
        var result = await _beerService.Add(beerInsertDto);

        // Retorna un resultado CreatedAtAction con el objeto BeerDto y la ruta del método GetById.
        return CreatedAtAction(nameof(GetById), new { id = result.BeerId }, result);
    }

    // Acción HTTP PUT para actualizar una cerveza por su ID.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBeer(int id, BeerUpdateDto beerUpdateDto)
    {
        // Delega la responsabilidad al servicio de cervezas.
        var result = await _beerService.Update(id, beerUpdateDto);

        // Retorna el resultado proporcionado por el servicio.
        return (IActionResult)result;
    }
}

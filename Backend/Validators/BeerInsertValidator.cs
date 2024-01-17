// Importa los namespaces necesarios
using Backend.DTOs;
using FluentValidation;

// Define una clase llamada BeerInsertValidator que hereda de AbstractValidator<BeerInsertDto>
public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
{
    // Constructor de la clase
    public BeerInsertValidator()
    {
        // Define una regla de validación para la propiedad BeerName del DTO BeerInsertDto
        // Asegura que BeerName no esté vacío
        RuleFor(x => x.BeerName).NotEmpty();
    }
}

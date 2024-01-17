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
        // Asegura que BeerName no esté vacío y proporciona un mensaje personalizado si la regla no se cumple
        RuleFor(x => x.BeerName).NotEmpty().WithMessage("El nombre es obligatorio");

        // Define una regla de validación para la propiedad BeerName del DTO BeerInsertDto
        // Asegura que BeerName tenga una longitud entre 2 y 20 caracteres y proporciona un mensaje personalizado si la regla no se cumple
        RuleFor(x => x.BeerName).Length(2, 20).WithMessage("El nombre debe tener de 2 a 20 caracteres");

        // Define una regla de validación para la propiedad BrandId del DTO BeerInsertDto
        // Asegura que BrandId no sea nulo, sea mayor que 0 y proporciona un mensaje personalizado si la regla no se cumple
        RuleFor(x => x.BrandId).NotNull().WithMessage(x => "La marca es obligatoria");
        RuleFor(x => x.BrandId).GreaterThan(0).WithMessage(x => "Error con el valor enviado de marca");

        // Define una regla de validación para la propiedad Alcohol del DTO BeerInsertDto
        // Asegura que Alcohol sea mayor que 0 y proporciona un mensaje personalizado si la regla no se cumple
        RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage(x => "El {PropertyName} debe ser mayor a 0 ");
    }
}

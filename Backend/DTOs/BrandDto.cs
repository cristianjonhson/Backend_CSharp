namespace Backend.DTOs
{
    // DTO para transferir datos de la entidad Brand
    public class BrandDto
    {
        // Propiedad para el identificador único de la marca
        public Int64 BrandId { get; set; }

        // Propiedad para el nombre de la marca
        public string? Name { get; set; }

        // Propiedad para la descripción de la marca
        public string? Description { get; set; }
    }
}


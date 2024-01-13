using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    // Define la clase StoreContext que hereda de DbContext.

    //StoreContext actúa como un punto de entrada para interactuar
    //con la base de datos y proporciona propiedades DbSet para cada entidad
    //(en este caso, Beer y Brand) que representan las tablas en la base de datos.
    public class StoreContext : DbContext
    {
        // Constructor que recibe opciones de configuración del contexto.
        // El constructor toma DbContextOptions como argumento, que se utiliza para
        // configurar el contexto de la base de datos.
        //
        // En este caso, StoreContext espera opciones específicas para su configuración.
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        // DbSet que representa la tabla de cervezas en la base de datos.
        public DbSet<Beer> Beers { get; set; }

        // DbSet que representa la tabla de marcas en la base de datos.
        public DbSet<Brand> Brands { get; set; }

    }
}

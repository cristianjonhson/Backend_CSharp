using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Backend.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la aplicaci�n
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//Repository

// Registra el repositorio de cervezas como implementacion de IBeerRepository
builder.Services.AddScoped<IBeerRepository<Beer>, BeerRepository>();

// Registra el repositorio de marcas como implementacion de IBrandRepository
builder.Services.AddScoped<IBrandRepository<Brand>, BrandRepository>();

// Agrega el contexto de la base de datos usando Entity Framework Core
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// Registra el servicio de marcas como implementacion de IBrandService
builder.Services.AddScoped<IBrandService, BrandService>();

// Registra el servicio de cervezas como implementacion de ICommonService
builder.Services.AddKeyedScoped<ICommonService<BeerDto,BeerInsertDto,BeerUpdateDto>, BeerService>("beerService");

// Agrega servicios al contenedor de dependencias.
builder.Services.AddControllers();

// Configura FluentValidation y registra el BeerInsertValidator como implementacion de IValidator<BeerInsertDto>
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();

// Configuracion y registro del servicio de posts
builder.Services.AddScoped<IPostsService, PostsService>();

// Configura y agrega un cliente HTTP para el servicio de posts
builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    // Configura la URL base para las solicitudes HTTP del servicio de posts
    c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");
});

// Configura y agrega Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura el middleware de Swagger y SwaggerUI en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configura el middleware para redireccionar a HTTPS
app.UseHttpsRedirection();

// Configura el middleware de autorizacion
app.UseAuthorization();

// Configura el middleware para mapear los controladores de la aplicacion
app.MapControllers();

// Ejecuta la aplicacion
app.Run();

using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Agrega tu servicio personalizado usando AddKeyedScoped para inyección de dependencias
builder.Services.AddKeyedScoped<IPeopleService, PeopleService>("peopleDervices");

// Inyección de dependencias para el servicio de posts
builder.Services.AddScoped<IPostsService, PostsService>();

// Configura y agrega un cliente HTTP para el servicio de posts
builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    // Configura la URL base para las solicitudes HTTP del servicio de posts
    c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");
});


//EntityFramework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//Validators
// Registra el BeerInsertValidator como implementación de IValidator<BeerInsertDto>
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();

// Agrega servicios al contenedor de dependencias.
builder.Services.AddControllers();

// Configura Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure el pipeline de solicitud HTTP.

// Habilita Swagger y SwaggerUI en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita la redirección HTTPS
app.UseHttpsRedirection();

// Habilita la autorización
app.UseAuthorization();

// Mapea los controladores de la aplicación
app.MapControllers();

// Ejecuta la aplicación
app.Run();

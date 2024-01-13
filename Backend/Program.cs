using Backend.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de dependencias.
builder.Services.AddControllers();

// Agrega tu servicio personalizado
//builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddKeyedScoped<IPeopleService, PeopleService>("peopleDervices");


//Inyeccion de dependencias de post
builder.Services.AddScoped<IPostsService, PostsService >();
// Configura Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

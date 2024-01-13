using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de dependencias.
builder.Services.AddControllers();

// Agrega tu servicio personalizado
builder.Services.AddScoped<IPeopleService, PeopleService>();

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

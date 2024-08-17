using TvShowApi.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Agregamos el repositorio en el contenedor de DI
builder.Services.AddSingleton<ITvShowRepository, TvShowRepository>();

// Agregar los controladores
builder.Services.AddControllers();

// Swagger para la documentación
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200") // Reemplaza con el origen de tu frontend
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configurar el pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TvShow API V1");
        c.RoutePrefix = string.Empty; // Esto permite que Swagger cargue en la raíz (opcional)
    });
}

// Aplicar CORS antes de cualquier middleware que pueda manejar las solicitudes
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

app.Run();

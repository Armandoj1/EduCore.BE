using EduCore.Web.Negocio;
using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Repositorio;
using EduCore.Web.Repositorio.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:4200") // URL del front-end
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddTransient<IDocentesBLL, DocentesBLL>();
builder.Services.AddTransient<IDocentesDAL, DocentesDAL>();
builder.Services.AddTransient<IDocenteEspecialidadDAL, DocenteEspecialidadDAL>();
builder.Services.AddTransient<IDocenteEspecialidadBLL, DocenteEspecialidadBLL>();
builder.Services.AddTransient<IDocenteMateriasGradosDAL, DocenteMateriasGradosDAL>();
builder.Services.AddTransient<IDocenteMateriasGradosBLL, DocenteMateriasGradosBLL>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplica la política de CORS
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();

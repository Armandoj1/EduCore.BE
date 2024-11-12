using EduCore.Web.Negocio;
using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Negocio.Interfaces.Notas;
using EduCore.Web.Negocio.Interfaces.Usuario;
using EduCore.Web.Negocio.Notas;
using EduCore.Web.Negocio.Usuarios;
using EduCore.Web.Repositorio;
using EduCore.Web.Repositorio.Interface;
using EduCore.Web.Repositorio.Interface.Notas;
using EduCore.Web.Repositorio.Interface.Usuarios;
using EduCore.Web.Repositorio.Notas;
using EduCore.Web.Repositorio.Usuarios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMedicosBLL, MedicosBLL>();
builder.Services.AddTransient<IMedicosDAL, MedicosDAL>();
builder.Services.AddTransient<IUsuariosBLL, UsuariosBLL>();
builder.Services.AddTransient<IUsuariosDAL, UsuariosDAL>();
builder.Services.AddTransient<INotasBLL, NotasBLL>();  
builder.Services.AddTransient<INotasDAL, NotasDAL>();  


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
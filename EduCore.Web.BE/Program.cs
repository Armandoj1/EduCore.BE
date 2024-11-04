using EduCore.Web.Negocio;
using EduCore.Web.Negocio.Interfaces;
using EduCore.Web.Repositorio;
using EduCore.Web.Repositorio.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMedicosBLL, MedicosBLL>();
builder.Services.AddTransient<IMedicosDAL, MedicosDAL>();


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
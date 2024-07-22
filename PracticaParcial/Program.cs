using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeccion de FluentValidations
builder.Services.AddFluentValidation((options) =>
options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

//Establece la config para hacer el manejo de errores ActionResult de la API con los definidos en los validadores
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//TODO 1: Configurar conexion y ejecutar scaffolding

// TODO 2: Injeccion de Context
builder.Services.AddDbContext < "Nombre_de_la_clase_context" > (options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDatabase")));

// TODO 4: Injeccion de repositorios
builder.Services.AddScoped<IObraRepository, ObraRepository>();
builder.Services.AddScoped<IAlbanilRepository, AlbanilRepository>();

// TODO 9: Injeccion del ParcialService
builder.Services.AddScoped<IParcialService, ParcialService>();

//Injectamos AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Configuramos CORS para aceptar peticiones de cualquier origen
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// TODO 3: Hacer Repositories Interfaces y luego Repositories Impl

// TODO 5: Crear DTOS

// TODO 7: Hacer los Validators con los DTOS

// TODO 8: Hacer Service Interface y luego Service Impl


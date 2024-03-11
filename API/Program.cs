using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//adicionando o DataContext como um Service
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Habilitando CORS
builder.Services.AddCors(); // Continuar no http pipeline, usar na ordem correta


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer(); REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER
// builder.Services.AddSwaggerGen(); REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER
// {
//     app.UseSwagger(); REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER
//     app.UseSwaggerUI(); REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER
// }

app.UseHttpsRedirection();

app.UseAuthorization();

//Continuando configuracao do CORS
app.UseCors(
    corsPolicyBuilder =>
             corsPolicyBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("https://localhost:4200")
);

app.MapControllers();

app.Run();

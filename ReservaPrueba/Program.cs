using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReservaPrueba.Models; 
using ReservaPrueba.Repositories;
using ReservaPrueba.Services;     
using System.Data;
using UsuarioAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDB")));

builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("connectionDB")));


builder.Services.AddScoped<ReservaRepository>();
builder.Services.AddScoped<ReservaService>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseMiddleware<ExcepcionesGlobales>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsuarioApi v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();


app.UseCors("ReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(p => p.AddPolicy("corsenabled", options =>
{
    options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDbContext<PersonajesContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("MySQL")));
builder.Services.AddTransient<RepositoryPersonajes>();
builder.Services.AddOpenApi();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.MapScalarApiReference(opt =>
{
    opt.Title = "Scalar Personajes";
    opt.Theme = ScalarTheme.BluePlanet;
});
app.UseCors("corsenabled");
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

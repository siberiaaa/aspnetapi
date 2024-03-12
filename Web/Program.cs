using System.Reflection;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(ICharacterRepository), typeof(CharacterRepository));
builder.Services.AddScoped(typeof(ICharacterService), typeof(CharacterService));
builder.Services.AddScoped(typeof(IEnemyRepository), typeof(EnemyRepository));
builder.Services.AddScoped(typeof(IEnemyService), typeof(EnemyService));
builder.Services.AddScoped(typeof(ICharacterTypeRepository), typeof(CharacterTypeRepository));
builder.Services.AddScoped(typeof(ICharacterTypeService), typeof(CharacterTypeService));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));



builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

builder.Services.AddControllers();



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Gracosoft ASP.NET Core API",
        Description = ".NET Core app made in classes. An API made for logic management for a RPG videogame.",
        TermsOfService = new Uri("https://cdn.discordapp.com/attachments/945837265593192499/1164992910425595974/1697826347808.jpg?ex=65453b32&is=6532c632&hm=a7fa6195635aca6e7fbde2d2a63060129a264bf043ac0a63ba1a3e8bb41dcc79&"),
        Contact = new OpenApiContact
        {
            Name = "Siberia",
            Url = new Uri("https://github.com/siberiaaa")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});





var app = builder.Build();

app.MapControllers();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Api de weather idk
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }

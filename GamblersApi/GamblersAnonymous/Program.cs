using Microsoft.OpenApi.Models;
using GamblersAnonymous;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gamblers Anonymous API", Description = "API for the Texas Hold'em Poker Game", Version = "v1" });
});



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gamblers Anonymous API V1");
});

app.MapGet("/", () => "Hello World!");
app.MapPost("/game", (Game game) => game.startGame());
app.MapGet("/games", () => "List of games");
app.MapGet("/games/{id}", (int id) => $"Game {id}");



app.Run();

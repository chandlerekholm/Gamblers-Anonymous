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
app.MapGet("/api/v1/players", () => Players.GetPlayers());
app.MapGet("/api/v1/players/{id}", (int id) => Players.GetPlayer(id));
app.MapPost("/api/v1/players", (Player player) => Players.AddPlayer(player));
app.MapPut("/api/v1/players/{id}", (int id, Player player) => Players.UpdatePlayer(id, player));
app.MapDelete("/api/v1/players/{id}", (int id) => Players.DeletePlayer(id));


app.Run();

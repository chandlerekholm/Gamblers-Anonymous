using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;
using GamblersAnonymous;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gamblers Anonymous API", Description = "API for the Texas Hold'em Poker Game", Version = "v1" });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gamblers Anonymous API V1");
});


Game game = null;
int numPlayers = 0;
app.MapGet("/", () => "Hello World!");
app.MapGet("/isGameStarted", () =>
{

   if (numPlayers == 2)
   {
      return true;
   }
   else
   {
      numPlayers++;
      return false;
   }
});
app.MapGet("/players", () => 
{

   game = new Game(200, 10, 5); // buy in amount, big blind, small blind
   game.startGame(); // start the game

   return JsonSerializer.Serialize(game.players);
   

});
app.MapGet("/communityCards", () => 
{
   if (game != null){
      return JsonSerializer.Serialize(game.communityCards);
   }
   else{
      return "Game has not started";
   }
});

app.Run();

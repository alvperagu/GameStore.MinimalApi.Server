using GameStore.MinimalApi.Server.Endpoints;
using GameStore.MinimalApi.Server.Data;

var builder = WebApplication.CreateBuilder(args);

var connString= builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();
app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();
app.Run();

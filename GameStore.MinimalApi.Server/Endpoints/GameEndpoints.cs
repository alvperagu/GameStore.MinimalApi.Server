using GameStore.MinimalApi.Server.Data;
using GameStore.MinimalApi.Server.Dtos;
using GameStore.MinimalApi.Server.Entities;
using GameStore.MinimalApi.Server.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.MinimalApi.Server.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetName";

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("games").WithParameterValidation();
            //Get /games
            group.MapGet("/", async (GameStoreContext dbContext) =>
                    await dbContext.Games
                        .Include(games => games.Genre)
                        .Select(x => x.ToGameSummaryDto())
                        .AsNoTracking()
                        .ToListAsync());

            //Get /games/id
            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                Game? game = await dbContext.Games.FindAsync(id);
                return game is null
                ? Results.NotFound()
                : Results.Ok(game.ToGameDetailsDto());
            }).WithName(GetGameEndpointName);

            //Post /games
            group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                var game = newGame.ToEntity();
                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(GetGameEndpointName,
                new { id = game.Id }, game.ToGameDetailsDto());
            });

            //Put games/1
            group.MapPut("/{id}", async (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FindAsync(id);
                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updateGameDto.ToEntity(id));
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            //Delete /games/1
            group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Games
                        .Where(game => game.Id == id)
                        .ExecuteDeleteAsync();

                return Results.NoContent();
            });
            return group;
        }
    }
}

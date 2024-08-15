using GameStore.MinimalApi.Server.Dtos;
using GameStore.MinimalApi.Server.Entities;

namespace GameStore.MinimalApi.Server.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {

        return new()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new(game.Id,
        game.Name,
        game.Genre!.Name,
        game.Price,
        game.ReleaseDate);
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

    public static Game ToEntity(this UpdateGameDto game, int id)
    {

        return new()
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

}

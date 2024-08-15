using GameStore.MinimalApi.Server.Data;
using GameStore.MinimalApi.Server.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.MinimalApi.Server.Endpoints;

public static class GenresEndPoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext) =>
         await dbContext.Genres
         .Select(genre => genre.ToDto())
         .AsNoTracking()
         .ToListAsync());
        return group;
    }

}

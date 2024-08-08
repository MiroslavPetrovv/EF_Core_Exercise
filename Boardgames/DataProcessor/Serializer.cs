namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Text;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context.Sellers
                .Where(s => s.BoardgamesSellers
                      .Any(bg => bg.Boardgame.YearPublished >= year || bg.Boardgame.Rating <= rating))
                    .Select(s => new
                    {
                        Name = s.Name,
                        Website = s.Website,
                        Boardgames = s.BoardgamesSellers
                        .Where(bg => bg.Boardgame.YearPublished >= year || bg.Boardgame.Rating <= rating)
                        .Select(bg => new
                        {
                            Name = bg.Boardgame.Name,
                            Rating = bg.Boardgame.Rating,
                            Mechanics = bg.Boardgame.Mechanics,
                            Category = bg.Boardgame.CategoryType
                        })
                        .OrderByDescending(bg => bg.Rating)
                        .ThenBy(bg => bg.Name)
                        .ToList()
                    })
                    .OrderByDescending(s => s.Boardgames.Count)
                    .ThenBy(s=> s.Name)
                    .ToList();

                return JsonConvert.SerializeObject(sellers, Formatting.Indented);
        }
    }
}
namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.DataProcessor.ImportDto;
    using Boardgames.Utilities;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
XmlHelper xmlHelper = new XmlHelper();
const string root = "Creators";

ICollection<Creator> creators = new List<Creator>();
ImportCreatorDto[] deserializeCreators =
    xmlHelper.Deserialize<ImportCreatorDto[]>(xmlString, root);

foreach (ImportCreatorDto creatorDto in deserializeCreators)
{
  

    if (!IsValid(creatorDto))
    {
        sb.AppendLine(ErrorMessage);
        continue;
    }
        ICollection<Boardgame> boardgamesToImport = new List<Boardgame>();
        foreach (BoardgameDto b in creatorDto.Boardgames)
    {
        if (!IsValid(b))
        {
            sb.AppendLine(ErrorMessage);
            continue;
        }

        Boardgame boardgame = new Boardgame()
        {
            Name = b.Name,
            Rating = b.Rating,
            YearPublished = b.YearPublished,
            CategoryType = (CategoryType)b.CategoryType,
            Mechanics = b.Mechanics
        };
        boardgamesToImport.Add(boardgame);
    }
        Creator creator = new Creator()
        {
            FirstName = creatorDto.FirstName,
            LastName = creatorDto.LastName,
            Boardgames = boardgamesToImport
        };

    creators.Add(creator);
    sb.AppendLine(string.Format(SuccessfullyImportedCreator,
        creatorDto.FirstName,creatorDto.LastName,creator.Boardgames.Count()));
    //apendLine
}
context.Creators.AddRange(creators);
context.SaveChanges();

return sb.ToString();

        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
ICollection<Seller> sellersToImport = new List<Seller>();
ImportSellerDto[] importSellertDto =
    JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString)!;
foreach (ImportSellerDto sellerDto in importSellertDto)
{
    if (!IsValid(sellerDto))
    {
        sb.AppendLine(ErrorMessage);
        continue;
    }
    Seller seller = new Seller()
    {
        Name = sellerDto.Name,
        Address = sellerDto.Address,
        Country = sellerDto.Country,
        Website = sellerDto.Website,

    };
    ICollection<BoardgameSeller> boardgamesToImport = new List<BoardgameSeller>();
    foreach(int boardgameId in sellerDto.Boardgames.Distinct())
    {
        if (!context.Boardgames.Any(b=> b.Id==boardgameId))
        {
            sb.AppendLine(ErrorMessage);
            continue;
        }
        BoardgameSeller boardgame = new BoardgameSeller()
        {
            Seller =seller,
            BoardgameId = boardgameId

        };
        boardgamesToImport.Add(boardgame);
    }
    seller.BoardgamesSellers = boardgamesToImport;

    sellersToImport.Add(seller);
    sb.AppendLine(string.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count()));

   
}
context.Sellers.AddRange(sellersToImport);
context.SaveChanges();

return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}

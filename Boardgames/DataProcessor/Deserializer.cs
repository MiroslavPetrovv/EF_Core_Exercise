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
                foreach (BoardgameDto b in creatorDto.Boardgames)
                {
                    if (!IsValid(b))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                }
                
            }

            return sb.ToString();

        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            return "";
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}

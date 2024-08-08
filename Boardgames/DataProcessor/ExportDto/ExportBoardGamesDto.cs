using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ExportDto
{
    using static Boardgames.Data.DataConstraints;
    public class ExportBoardGamesDto
    {
        //[XmlElement(nameof(Name))]
        //[MaxLength(BoardGameNameMaxLength)]
        //[MinLength(BoardGameNameMinLength)]
        //[Required]
        //public string Name { get; set; } = null!;

        //[XmlElement()]
        //public int YearPublished { get; set; }
    }
}

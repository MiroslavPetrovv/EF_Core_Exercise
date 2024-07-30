using Boardgames.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    using static Boardgames.Data.DataConstraints;
    [XmlType("Boardgame")]
    public class BoardgameDto
    {
        [XmlElement(nameof(Name))]
        [MaxLength(BoardGameNameMaxLength)]
        [MinLength(BoardGameNameMinLength)]
        [Required]
        public string Name { get; set; } = null!;

        [XmlElement(nameof(Rating))]
        [Range(BoardGameRatingMinValue,BoardGameRatingMaxValue)]
        [Required]
        public decimal Rating { get; set; }

        [XmlElement(nameof(YearPublished))]
        [Range(BoardGameYearPublishedMinValue,BoardGameYearPublishedMaxValue)]
        [Required]

        public int YearPublished { get; set; }

        [XmlElement(nameof(CategoryType))]
        [Required]
        public int CategoryType { get; set; }

        [Required]
        public string Mechanics { get; set; } = null!;


    }
}
